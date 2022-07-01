using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;

namespace BLL.Models
{
    public class DishMenuModel
    {
        public int Id { get; set; }
        public int Dish { get; set; }
        public int Menu { get; set; }
        public DishMenuModel() { }
        public DishMenuModel(MenuDish dm) {
            Id = dm.Id;
            Dish = dm.Dish;
            Menu = dm.Menu;
        }
    }
}