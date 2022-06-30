using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;
using DAL.Repository;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using BLL.Models;

 
namespace Dinner.Controllers
{
    [ApiController]
    [Route("api/dinnermenu")]
    public class DinnerMenuController : ControllerBase
    {
        private readonly IRepository<DinnerMenu> _dinnerMenuRepos;
        private readonly IRepository<DishMenu> _dishMenuRepos;
        private readonly IRepository<Dish> _dishRepos;
        public DinnerMenuController(IRepository<DinnerMenu> dinnerMenuRepos, IRepository<DishMenu> dishMenuRepos, IRepository<Dish> dishRepos)
        {
            _dinnerMenuRepos = dinnerMenuRepos;
            _dishMenuRepos = dishMenuRepos;
            _dishRepos = dishRepos;       
        }
        

        //api/dinnermenu/2022-07-12
        [HttpGet("{date}")]
        public List<DishModel> Get(DateTime date)
        {
            List<DishModel> returnDishesFromMenu = new List<DishModel>();
            DinnerMenu dinnerMenu = (from d in _dinnerMenuRepos.GetAll() where d.Date == date select d).First();
            List<DishModel> dishesFromMenu = (from dishId in _dishMenuRepos.GetAll()
                                        from dish in _dishRepos.GetAll()
                                        where dishId.Menu == dinnerMenu.Id && dishId.Dish == dish.Id
                                        select new DishModel {Id = dish.Id, Name = dish.Name, Position = dish.Position, Price = dish.Price}).ToList();

            if (dishesFromMenu.Count() != 0)
            { 
                return dishesFromMenu;
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Post(DateTime date, List<int> dishesList)
        {
            DinnerMenu dinnerMenuItems = new DinnerMenu
            {
                Date = date,
            };

            _dinnerMenuRepos.Create(dinnerMenuItems);
            if (dinnerMenuItems.Id == 0)
                return BadRequest();

            List<DishMenu> dishMenuItems = new List<DishMenu>();

            foreach (int d in dishesList)
            {
                dishMenuItems.Add(new DishMenu 
                {
                    Menu = dinnerMenuItems.Id,
                    Dish = d
                });
            }
            try
            {                
                foreach (DishMenu i in dishMenuItems)
                {
                    _dishMenuRepos.Create(i);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{date}/{disheslist}")]
        public async Task<IActionResult> Put(DateTime date, List<int> dishesList)
        {
            DinnerMenu dinnerMenu = (from d in _dinnerMenuRepos.GetAll() where d.Date == date select d).First();
            List<DishMenu> dishMenuItems = new List<DishMenu>();
            dishMenuItems = (from dishId in _dishMenuRepos.GetAll()
                                            from dish in _dishRepos.GetAll()
                                            where dishId.Menu == dinnerMenu.Id && dishId.Dish == dish.Id
                                            select new DishMenu {Id = dish.Id}).ToList();
            try
            {
                foreach (DishMenu d in dishMenuItems)
                {
                    _dishMenuRepos.Delete(d.Id);
                }
                foreach (int id in dishesList)
                {
                    _dishMenuRepos.Create(new DishMenu 
                    {
                        Dish = id,
                        Menu = dinnerMenu.Id
                    });
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        //api/dinnermenu/2022-07-12
        [HttpGet("{dateFirst}/{dateSecond}")]
        public Dictionary<DateTime, List<DishModel>> Get(DateTime dateFirst, DateTime dateSecond)
        {
            List<DishModel> returnDishesFromMenu = new List<DishModel>();
            List<DinnerMenu> dinnerMenu = (from d in _dinnerMenuRepos.GetAll() where d.Date >= dateFirst & d.Date <= dateSecond select d).ToList();
            Dictionary<DateTime, List<DishModel>> datesAndDishesDict = new Dictionary<DateTime, List<DishModel>>();
            for (int i = 0; i < dinnerMenu.Count(); i++)
            { 
                List<DishModel> dishesFromMenu = (from dishId in _dishMenuRepos.GetAll()
                                            from dish in _dishRepos.GetAll()
                                            where dishId.Menu == dinnerMenu[i].Id && dishId.Dish == dish.Id
                                            select new DishModel {
                                                Id = dish.Id, 
                                                Name = dish.Name, 
                                                Position = dish.Position, 
                                                Price = dish.Price}).ToList();
                datesAndDishesDict.Add(dateFirst, dishesFromMenu);
                dateFirst = dateFirst.AddDays(1);
            }

            if (datesAndDishesDict.Count() != 0)
            { 
                return datesAndDishesDict;
            }
            return null;
        }

        [HttpDelete("{date}")]
        public async Task<IActionResult> Delete(DateTime date)
        {
            DinnerMenu dinnerMenu = (from d in _dinnerMenuRepos.GetAll() where d.Date == date select d).First();
            
            try
            {                
                _dinnerMenuRepos.Delete(dinnerMenu.Id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dateFirst}/{dateSecond}")]
        public async Task<IActionResult> Delete(DateTime dateFirst, DateTime dateSecond)
        {
            List<DinnerMenu> dinnerMenu = (from d in _dinnerMenuRepos.GetAll() where d.Date >= dateFirst & d.Date <= dateSecond select d).ToList();
            try
            {         
                foreach (DinnerMenu menu in dinnerMenu)
                {       
                    _dinnerMenuRepos.Delete(menu.Id);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
