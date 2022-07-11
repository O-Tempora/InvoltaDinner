using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using DAL.Interfaces;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BLL.Services;

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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeUserBalance ([FromBody] ChangeUserBalanceModel changeUserBalance)
        {
            if(ModelState.IsValid)
            {
                _iDbCrud.ChangeUserBalance(changeUserBalance.AdminId, changeUserBalance.UserId, changeUserBalance.Price, changeUserBalance.Date);

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
        [HttpPost("roles")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeUserRole ([FromBody] ChangeRoleModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _iDbCrud.GetUserById(model.UserId);
                if(user != null)
                {
                    if(user.IsApproved == false)
                    {
                        return BadRequest("Нельзя менять роль у неподтвержденного пользователя!");
                    }   
                    var changed_user = new UserModel 
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Role = model.Role,
                        IsApproved = user.IsApproved,
                        RefreshToken = user.RefreshToken
                    };
                    _iDbCrud.ChangeUserRole(changed_user);
                    var new_token = TokenService.GenerateJWT(changed_user);
                    return Ok(new_token);
                }
                else 
                {
                    return BadRequest("Такого пользователя не существует");
                }
            }
            else
            {
                return BadRequest("Ошибка изменения пользователя");
            }
        }
    }
}