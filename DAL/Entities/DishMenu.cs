using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class DishMenu
    {
        public int Id { get; set; }
        public int Dish { get; set; }
        public int Menu { get; set; }

        public virtual Dish DishNavigation { get; set; } = null!;
        public virtual DinnerMenu MenuNavigation { get; set; } = null!;
    }
}
