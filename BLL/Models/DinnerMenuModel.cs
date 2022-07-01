using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;

namespace BLL.Models
{
    public class DinnerMenuModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        public DinnerMenuModel() { }
        public DinnerMenuModel(Menu dm)
        {
            Id = dm.Id;
            Date = dm.Date;
        }
    }
}