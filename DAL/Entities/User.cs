using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class User
    {
        public User()
        {
            Records = new HashSet<Record>();
        }

        public int Id { get; set; }
        public string Password { get; set; } = null!;
        public decimal Debt { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
