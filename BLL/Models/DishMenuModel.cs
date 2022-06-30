using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Models
{
    public class DishMenuModel
    {
        public int Id { get; set; }
        public int Dish { get; set; }
        public int Menu { get; set; }
        
        public DishModel DishNavigation { get; set; }
        public DinnerMenuModel MenuNavigation { get; set; }
        public DishMenuModel() { }
        public DishMenuModel(DishMenu dm) {
            Id = dm.Id;
            Dish = dm.Dish;
            Menu = dm.Menu;
            DishNavigation = new DishModel(dm.DishNavigation);
            MenuNavigation = new DinnerMenuModel(dm.MenuNavigation);
        }
    }
}