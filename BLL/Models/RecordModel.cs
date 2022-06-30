using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class RecordModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public decimal? Price { get; set; }
        
        public RecordModel() { }
        public RecordModel(Record r) { 
            Id = r.Id;
            Date = r.Date;
            UserId = r.UserId;
            Price = r.Price;
        }
    }
}