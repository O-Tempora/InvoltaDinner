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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public UserController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }
        //api/dinnermenu/2022-07-12
        [HttpGet]
        public List<UserModel> GetAll()
        {
            List<UserModel> returnAllUsers = _iDbCrud.GetAllUsers();
            if (returnAllUsers.Count() != 0)
            { 
                return returnAllUsers;
            }
            return null;
        }
        //api/dinnermenu/2022-07-12
        [HttpGet("id")]
        public UserModel GetUser(int id)
        {
            UserModel returnUser = _iDbCrud.GetUser(id);
            return returnUser;
        }
    }
}
