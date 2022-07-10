using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
     public class UpdateRecordStatusModel
     {
        public int Id { get; set; }
        public sbyte Status { get; set; }

        public UpdateRecordStatusModel() { }
        public UpdateRecordStatusModel(int id, sbyte status) 
        {
            Id = id;
            Status = status;
        }

     }
}