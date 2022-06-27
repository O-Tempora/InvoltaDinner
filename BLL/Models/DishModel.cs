using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Models
{
    public class DishModel
    {
        public int Id {get;set;}
        public int Position {get;set;}
        public string? Name {get;set;}
        public decimal Price {get;set;}
        public string? ShowPrice{get;set;}
        public DishModel() { }
        public DishModel(Dish dish)
        {
            Id = dish.Id;
            Position = dish.Position;
            Name = dish.Name;
            Price = dish.Price;
            ShowPrice = $"Цена: {Price} руб.";
        }
    }
}