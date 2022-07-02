using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class Menu
    {
        public Menu()
        {
            MenuDishes = new HashSet<MenuDish>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public sbyte IsActive { get; set; }

        public virtual ICollection<MenuDish> MenuDishes { get; set; }
    }
}
