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

    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public TransactionController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }

        [HttpGet]
        public List<TransactionModel> Get(int User)
        {
            return _iDbCrud.GetUserTransations(User);
        }
    }
}
