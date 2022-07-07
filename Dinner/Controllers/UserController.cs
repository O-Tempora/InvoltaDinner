using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BLL.Models;
using BLL.Interfaces;
using BLL;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

 
namespace Dinner.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public UserController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }
        
        [HttpGet("id")]
        public UserModel GetUserById(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);
            int id = Int32.Parse(jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);

            UserModel returnUser = _iDbCrud.GetUserById(id);
            return returnUser;
        }

        [HttpGet]
        public (string UserName, decimal Balance, int Id) GetUserData(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);
            var tuple = (
                UserName: jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Name).Value, 
                Balance: Convert.ToDecimal(jwt.Claims.First(claim => claim.Type == "balance").Value),
                Id: Int32.Parse(jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value));
            return tuple;
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetUserBalance(int userId)
        {
            if(ModelState.IsValid)
            {
                decimal balance = _iDbCrud.GetUserBalance(userId);
                
                return new ObjectResult(balance);
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
