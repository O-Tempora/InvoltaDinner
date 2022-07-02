using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class Dish
    {
        public Dish()
        {
            MenuDishes = new HashSet<MenuDish>();
            RecordDishes = new HashSet<RecordDish>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Position { get; set; }

        public virtual ICollection<MenuDish> MenuDishes { get; set; }
        public virtual ICollection<RecordDish> RecordDishes { get; set; }
    }
}
