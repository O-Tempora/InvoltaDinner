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

    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public TransactionController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        public List<TransactionModel> Get(int User)
        {
            return _iDbCrud.GetUserTransactions(User);
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "admin")]
        public List<TransactionModel> GetAll()
        {
            return _iDbCrud.GetAllTransactions();
        }
    }
}
