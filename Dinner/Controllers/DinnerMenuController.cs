using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BLL.Models;
using BLL.Interfaces;
using BLL;

 
namespace Dinner.Controllers
{
    [ApiController]
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public MenuController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }
        
        [HttpGet]
        public Tuple<List<MenuModel>, List<MenuModel>> GetCurrentAndNextMonth()
        {
            return _iDbCrud.GetPeriodMenu();
        }

        // [HttpGet("{date}")]
        // public List<DishModel> Get(DateTime date)
        // {
        //     List<DishModel> returnDishesFromMenu = _iDbCrud.GetDishesByDate(date);
        //     if (returnDishesFromMenu.Count() != 0)
        //     { 
        //         return returnDishesFromMenu;
        //     }
        //     return null;
        // }
        
        [HttpPost]
        public async Task<IActionResult> Post(DateTime date, List<int> dishesList)
        {
            try
            {                
                _iDbCrud.CreateDishAndDinnerMenu(date, dishesList);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{date}/{dishesList}")]
        public async Task<IActionResult> Put(DateTime date, List<int> dishesList)
        {
            try
            {
                _iDbCrud.UpdateDishMenu(date, dishesList);      
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpGet("{dateFirst}/{dateSecond}")]
        public Dictionary<DateTime, List<DishModel>> GetPeriod(DateTime dateFirst, DateTime dateSecond)
        {
            Dictionary<DateTime, List<DishModel>> returnDishFromMenu = _iDbCrud.GetPeriodDish(dateFirst, dateSecond);
            if (returnDishFromMenu.Count() != 0)
            { 
                return returnDishFromMenu;
            }
            return null;

        }

        [HttpDelete("{date}")]
        public async Task<IActionResult> Delete(DateTime date)
        {
            try
            {
                _iDbCrud.DeleteDinnnerMenu(date);      
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dateFirst}/{dateSecond}")]
        public async Task<IActionResult> DeletePeriod(DateTime dateFirst, DateTime dateSecond)
        {
            try
            {
                _iDbCrud.DeletePeriodDinnnerMenu(dateFirst, dateSecond);      
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
