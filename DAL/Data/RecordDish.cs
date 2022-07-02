using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class RecordDish
    {
        public int Id { get; set; }
        public int Dish { get; set; }
        public int Record { get; set; }

        public virtual Dish DishNavigation { get; set; } = null!;
        public virtual Record RecordNavigation { get; set; } = null!;
    }
}
