using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using BLL.Interfaces;
using DAL.Data;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BLL.Services;

namespace Dinner.Controllers
{
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public AccountController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }

        [HttpPost]
        [Route("api/account/signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel user)
        {
            if (_iDbCrud.CheckUserByEmail(user.Email) != true)
            {
                if (user.Password.Length <= 30 && user.Password.Length >= 6)
                {
                    if (ModelState.IsValid) {
                        _iDbCrud.CreateUser(user);
                        return Ok("Новый пользователь добавлен!");
                    }
                }
                else return BadRequest("Длина пароля должна быть от 6 до 30 символов!");
            }
            else return BadRequest("Аккаунт с данной электронной почтой уже существует!");
            return BadRequest("Не удалось добавить пользователя");
        }

        [HttpPost]
        [Route("api/account/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            UserModel AuthenticateUser (string email, string password)
            {
                return _iDbCrud.GetUserByEmailAndPassword(email, password);
            }

            var user = AuthenticateUser(model.Email, model.Password);

            if (user != null)
            {
                //Генерация токена
                var token = TokenService.GenerateJWT(user);
                var refreshToken = TokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                _iDbCrud.UpdateUser(user);
                return Ok(
                    (token, refreshToken)
                );
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("api/account/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] string email)
        {
            if (_iDbCrud.CheckUserByEmail(email) == true)
            {
                await _iDbCrud.ResetPasswordOfUser(email);
                return Ok("Новый пароль отправлен на электронную почту!");
            }
            else return BadRequest("Данного пользователя не существует!");
        }

        [HttpPut]
        [Route("api/account/changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            if (changePasswordModel.NewPassword.Length <= 30 && changePasswordModel.NewPassword.Length >= 6)
            {
                bool correctPassword = _iDbCrud.ChangePasswordOfUser(changePasswordModel);
                if (correctPassword == true)
                    return Ok("Ваш пароль изменён!");
                else return BadRequest("Старый пароль был введен неправильно!");
            }
            else return BadRequest("Длина пароля должна быть больше 5 и меньше 31 символа");
        }


        // private string GenerateJWT (UserModel user)
        // {
        //     var securityKey  = AuthOptions.GetSymmetricSecurityKey();
        //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // var claims = new List<Claim>() {
            //     new Claim(JwtRegisteredClaimNames.Email, user.Email),
            //     new Claim(JwtRegisteredClaimNames.Name, user.Name),
            //     new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            //     new Claim(ClaimTypes.Role, user.Role)
            // };

        //     var token = new JwtSecurityToken( AuthOptions.ISSUER,
        //         AuthOptions.AUDIENCE,
        //         claims,
        //         expires: DateTime.Now.AddSeconds(AuthOptions.LIFETIME),
        //         signingCredentials: credentials);

        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }
    }
}