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
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            

            return Ok();
        }

        [HttpPost]
        [Route("api/account/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var user = AuthenticateUser(model.Email, model.Password);

            if (user != null)
            {
                //Генерация токена
                var token = GenerateJWT(user);
                return Ok(new 
                {
                    access_token = token
                });
            }
            else
            {
                return Unauthorized();
            }
        }
        
        private User AuthenticateUser (string email, string password)
        {
            return _iDbCrud.GetUserByEmailAndPassword(email, password); //Поменять
        }

        private string GenerateJWT (User user)
        {
            var securityKey  = AuthOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            claims.Add(new Claim("role", user.Role));

            var token = new JwtSecurityToken( AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                claims,
                expires: DateTime.Now.AddSeconds(AuthOptions.LIFETIME),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}