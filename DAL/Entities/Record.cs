using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Record
    {
        public Record()
        {
            RecordDishes = new HashSet<RecordDish>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public decimal? Price { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<RecordDish> RecordDishes { get; set; }
    }
}
