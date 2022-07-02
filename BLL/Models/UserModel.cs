using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        
        public UserModel() { }
        public UserModel(User u) { 
            Id = u.Id;
            Balance = u.Balance;
            Email = u.Email;
            Password = u.Password;
            Name = u.Name;
            Role = u.Role;
        }
    }
}