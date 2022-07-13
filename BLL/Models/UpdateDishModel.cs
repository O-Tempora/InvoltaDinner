using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UpdateDishModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public UpdateDishModel() { }
        
        public UpdateDishModel(int id, string name, decimal price) { 
            Id = id;
            Name = name;
            Price = price;
        }
    }
}