using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public partial class User
    {
        public User()
        {
            Records = new HashSet<Record>();
            TransactionAdminNavigations = new HashSet<Transaction>();
            TransactionUserNavigations = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public sbyte IsApproved { get; set; }


        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Transaction> TransactionAdminNavigations { get; set; }
        public virtual ICollection<Transaction> TransactionUserNavigations { get; set; }
    }
}
