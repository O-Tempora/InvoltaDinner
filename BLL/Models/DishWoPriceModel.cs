using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class DishWoPriceModel
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public DishWoPriceModel() { }
        
        public DishWoPriceModel(int id, int position, string name) { 
            this.Id = id;
            this.Position = position;
            this.Name = name;
        }
    }
}