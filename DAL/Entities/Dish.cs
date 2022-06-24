using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Dish
    {
        public Dish()
        {
            DishMenus = new HashSet<DishMenu>();
            RecordDishes = new HashSet<RecordDish>();
        }

        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Position { get; set; }
        public int Id { get; set; }

        public virtual ICollection<DishMenu> DishMenus { get; set; }
        public virtual ICollection<RecordDish> RecordDishes { get; set; }
    }
}
