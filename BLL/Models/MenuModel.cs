using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;

namespace BLL.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive {get; set; }
        public MenuModel() { }
        public MenuModel(Menu m)
        {
            Id = m.Id;
            Date = m.Date;
            if (m.IsActive == 0)
            {
                IsActive = false;
            }
            else IsActive = true;
        }
    }
}