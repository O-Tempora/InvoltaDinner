using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class Record
    {
        public Record()
        {
            RecordDishes = new HashSet<RecordDish>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public sbyte IsReady { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<RecordDish> RecordDishes { get; set; }
    }
}
