using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class CreateDishModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Position { get; set; }

        public CreateDishModel() { }
        public CreateDishModel(string name, decimal price, int position) 
        {
            Name = name;
            Price = price;
            Position = position;
        }
    }
}