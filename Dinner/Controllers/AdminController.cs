using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using DAL.Interfaces;
using BLL.Interfaces;

namespace Dinner.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly IDbCrud _iDbCrud;
        
        public AdminController (IDbCrud dbCrud)
        {
            _iDbCrud = dbCrud;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            if(ModelState.IsValid)
            {
                List<UserModel> allUsers = _iDbCrud.GetAllUsers();
                
                return new ObjectResult(allUsers);
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

        [HttpPut("UserId")]
        public async Task<IActionResult> ApproveUser(int userId)
        {
            if(ModelState.IsValid)
            {
                _iDbCrud.ApproveUser(userId);

                var msg = new 
                {
                    message = "User account was confirmed"
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

        [HttpDelete("UserId")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if(ModelState.IsValid)
            {
                _iDbCrud.DeleteUser(userId);

                var msg = new 
                {
                    message = "Пользователь был удален"
                };
                return Ok(msg);
            }
            else {
                return BadRequest();
            }
        }

        [HttpPost("{adminId}/{userId}/{price}/{date}")]
        public async Task<IActionResult> ChangeUserBalance (int adminId,int userId, decimal price, DateTime date)
        {
            if(ModelState.IsValid)
            {
                _iDbCrud.ChangeUserBalance(adminId, userId, price, date);

                var msg = new 
                {
                    message = "User balance was changed"
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