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
        
        //api/dinnermenu/2022-07-12
        [HttpGet("id")]
        public UserModel GetUserById(int id)
        {
            UserModel returnUser = _iDbCrud.GetUserById(id);
            return returnUser;
        }
        // [HttpGet]
        // public UserModel GetUserData(string token)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var jwt = tokenHandler.ReadJwtToken(token);
        //     return new UserModel
        //         {   
        //             Id = Int32.Parse(jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value),
        //             Balance = Convert.ToDecimal(jwt.Claims.First(claim => claim.Type == "balance").Value),
        //             Email = jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value,
        //             Name = jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Name).Value,
        //             Role = jwt.Claims.First(claim => claim.Type == "role").Value
        //         };
        // }
        [HttpGet]
        public (string UserName, decimal Balance) GetUserData(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);
            var tuple = (
                UserName: jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Name).Value, 
                Balance:Convert.ToDecimal(jwt.Claims.First(claim => claim.Type == "balance").Value));
            return tuple;
        }
    }
}
