using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Models
{
    public class DinnerMenuModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        public DinnerMenuModel() { }
        public DinnerMenuModel(DinnerMenu dm)
        {
            Id = dm.Id;
            Date = dm.Date;
        }
    }
}