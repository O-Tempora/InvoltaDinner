using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class RecordNameModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public List<DishWoPriceModel> DishesList { get; set; }
        
        public RecordNameModel() { }
        public RecordNameModel(int id, string username, int status, List<DishWoPriceModel> dishes) { 
            this.UserName = username;
            this.DishesList = dishes;
            this.Id = id;
            this.Status = status;
        }
    }
}
