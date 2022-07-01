using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Admin { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public virtual User AdminNavigation { get; set; } = null!;
        public virtual User UserNavigation { get; set; } = null!;
    }
}
