using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class DinnerMenu
    {
        public DinnerMenu()
        {
            DishMenus = new HashSet<DishMenu>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<DishMenu> DishMenus { get; set; }
    }
}
