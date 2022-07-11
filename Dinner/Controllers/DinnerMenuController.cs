using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BLL.Models;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Authorization;

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
        
        [HttpPost]
        [Authorize(Roles = "cook,admin")]
        public async Task<IActionResult> Post([FromBody] CreateUpdateDishModel createUpdateDish)
        {
            try
            {                
                _iDbCrud.CreateDishAndDinnerMenu(createUpdateDish.Date, createUpdateDish.DishesList);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "cook,admin")]
        public async Task<IActionResult> Put([FromBody] CreateUpdateDishModel createUpdateDish)
        {
            try
            {
                _iDbCrud.UpdateDishMenu(createUpdateDish.Date, createUpdateDish.DishesList);      
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{date}")]
        public async Task<IActionResult> Put(DateTime date)
        {
            try
            {
                _iDbCrud.SwitchMenuStatus(date);      
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpGet("periodMenu")]
        public Dictionary<DateTime, List<DishModel>> GetPeriod([FromBody] PeriodModel period)
        {
            Dictionary<DateTime, List<DishModel>> returnDishFromMenu = _iDbCrud.GetPeriodDish(period.DateFirst, period.DateSecond);
            if (returnDishFromMenu.Count() != 0)
            { 
                return returnDishFromMenu;
            }
            return null;
        }

        [HttpDelete("{date}")]
        [Authorize(Roles = "cook,admin")]
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

        [HttpDelete]
        [Authorize(Roles = "cook,admin")]
        public async Task<IActionResult> DeletePeriod([FromBody] PeriodModel period)
        {
            try
            {
                _iDbCrud.DeletePeriodDinnnerMenu(period.DateFirst, period.DateSecond);      
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
