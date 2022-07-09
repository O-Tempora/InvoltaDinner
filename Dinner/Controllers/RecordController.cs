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

    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        private readonly IDbCrud _iDbCrud;
        public RecordController(IDbCrud iDbCrud)
        {      
            _iDbCrud = iDbCrud;
        }

        [HttpPost("updateMany")]
        public async Task<IActionResult> Post([FromBody]List<RecordInfoModel> recordInfoModels)
        {
            try
            {
                foreach (RecordInfoModel recordInfoModel in recordInfoModels)  
                { 
                    recordInfoModel.date = recordInfoModel.date.Date;             
                    _iDbCrud.CreateOrUpdateRecord(recordInfoModel.date, recordInfoModel.userId, recordInfoModel.position);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("updateOne")]
        public async Task<IActionResult> PostOneRecord([FromBody]RecordInfoModel recordInfoModel)
        {
            try
            {
                recordInfoModel.date = recordInfoModel.date.Date;             
                _iDbCrud.CreateOrUpdateRecord(recordInfoModel.date, recordInfoModel.userId, recordInfoModel.position);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cook")]
        public Tuple<List<RecordPosModel>, List<RecordPosModel>> GetCurrentAndNextMonth(int id)
        {
            return _iDbCrud.GetPeriodRecord(id);
        }        [HttpGet("recordsByDate")]
        public List<RecordNameModel> GetRecordsByDate(DateTime date)
        {
            return _iDbCrud.GetDayRecords(date);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, sbyte status)
        {
            try
            {
                _iDbCrud.UpdateRecordStatus(id, status);     
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
