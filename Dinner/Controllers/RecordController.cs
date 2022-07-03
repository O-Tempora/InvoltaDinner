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

    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public RecordController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<RecordInfoModel> recordInfoModels)
        {
            try
            {
                foreach (RecordInfoModel recordInfoModel in recordInfoModels)  
                { 
                    recordInfoModel.date = recordInfoModel.date.Date;             
                    _iDbCrud.CreateRecord(recordInfoModel.date, recordInfoModel.userId, recordInfoModel.position);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public Tuple<List<RecordPosModel>, List<RecordPosModel>> GetCurrentAndNextMonth(int id)
        {
            return _iDbCrud.GetPeriodRecord(id);
        }
    }
}
