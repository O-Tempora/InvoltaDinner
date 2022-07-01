using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class MenuDish
    {
        public int Id { get; set; }
        public int Menu { get; set; }
        public int Dish { get; set; }

        public virtual Dish DishNavigation { get; set; } = null!;
        public virtual Menu MenuNavigation { get; set; } = null!;
    }
}
