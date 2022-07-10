using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class CreateUpdateDishModel
    {
        public DateTime Date { get; set; }
        public List<int> DishesList { get; set; }

        public CreateUpdateDishModel() { }
        public CreateUpdateDishModel(DateTime date, List<int> dishesList) 
        {
            Date = date;
            DishesList = dishesList;
        }
    }
}