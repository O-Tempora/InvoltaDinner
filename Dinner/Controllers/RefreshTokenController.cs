using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BLL.Services;
using BLL.Models;

namespace Dinner.Controllers
{
    [ApiController]
    [Route("api/refreshToken")]
    public class RefreshTokenController : Controller
    {
        private readonly IDbCrud _iDbCrud;
        public RefreshTokenController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel) 
        {
            if(ModelState.IsValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(tokenModel.JWTtoken);
                var userId = Int32.Parse(jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);

                var user = _iDbCrud.GetUserById(userId);
                if(user.RefreshToken != tokenModel.RefreshToken)
                {
                    return BadRequest("Токены не совпадают");
                }

                var newJwtToken = TokenService.GenerateJWT(user);
                return Ok(newJwtToken);

            }
            else
            {
                return BadRequest("Ошибка обработки запроса");
            }
        }
    }
}