using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class Record
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
