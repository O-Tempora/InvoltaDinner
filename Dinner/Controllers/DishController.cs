using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using DAL.Interfaces;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Dinner.Controllers
{
    [ApiController]
    [Route("api/dish")]
    public class DishController : Controller
    {
        private readonly IDbCrud _iDbCrud;
        
        public DishController (IDbCrud dbCrud)
        {
            _iDbCrud = dbCrud;
        }

        [HttpPost]
        [Authorize(Roles = "cook")]
        public async Task<IActionResult> Post (string name, decimal price, int position) 
        {

            if(ModelState.IsValid)
            {
                _iDbCrud.CreateDish(name, price, position);

                var msg = new 
                {
                    message = "Your dish was added to db"
                };
                return Ok(msg);
            }
            else{
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return BadRequest(errorMsg);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(ModelState.IsValid)
            {
                DishModel dish = _iDbCrud.GetDish(id);

                return new ObjectResult(dish);
            }
            else{
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return BadRequest(errorMsg);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDishes()
        {
            if(ModelState.IsValid)
            {
                List<DishModel> allDishes = _iDbCrud.GetAllDishes();

                return new ObjectResult(allDishes);
            }
            else{
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return BadRequest(errorMsg);
            }
        }

        [HttpDelete ("{id}")]
        [Authorize(Roles = "cook")]
        public async Task<IActionResult> Delete(int id)
        {
            if(ModelState.IsValid)
            {
                _iDbCrud.DeleteDish(id);

                var msg = new 
                {
                    message = "Your dish was deleted from db"
                };
                return Ok(msg);
            }
            else{
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return BadRequest(errorMsg);
            }
        }
    }
}