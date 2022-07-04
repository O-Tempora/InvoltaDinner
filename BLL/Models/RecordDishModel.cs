using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;

namespace BLL.Models
{
    public class RecordDishModel
    {
        public int Id { get; set; }
        public int Dish { get; set; }
        public int Record { get; set; }
        public RecordDishModel() { }
        public RecordDishModel(RecordDish rd) {
            Id = rd.Id;
            Dish = rd.Dish;
            Record = rd.Record;
        }
    }
}