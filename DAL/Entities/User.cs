using System.Security.Principal;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Records = new HashSet<Record>();
        }

        //public int Id { get; set; }
        public string Password { get; set; } = null!;
        public decimal Debt { get; set; }

        public virtual ICollection<Record> Records { get; set; }
    }
}
