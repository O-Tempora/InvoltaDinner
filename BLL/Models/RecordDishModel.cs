using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Models
{
    public class RecordDishModel
    {
        public int Id { get; set; }
        public int Record { get; set; }
        public int Dish { get; set; }
        public DishModel DishNavigation { get; set; }
        public RecordModel RecordNavigation { get; set; }

        public RecordDishModel() {} 
        public RecordDishModel(RecordDish rd) {
            Id = rd.Id;
            Record = rd.Record;
            Dish = rd.Dish;
            DishNavigation = new DishModel(rd.DishNavigation);
            RecordNavigation = new RecordModel(rd.RecordNavigation);
        }
    }
}