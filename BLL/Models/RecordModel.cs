using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class RecordModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public sbyte isReady { get; set; }
        public DateTime Date { get; set; }
        public sbyte IsReady { get; set; }
        
        public RecordModel() { }
        public RecordModel(Record r) { 
            Id = r.Id;
            UserId = r.UserId;
            Price = r.Price;
            Date = r.Date;
            isReady = 0;
        }
    }
}