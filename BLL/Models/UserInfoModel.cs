using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;

namespace BLL.Models
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        // public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool IsApproved {get; set;}
        public decimal Balance {get; set;}

        
        public UserInfoModel() { }
        public UserInfoModel(UserModel u, decimal balance) { 
            Id = u.Id;
            Email = u.Email;
            Name = u.Name;
            Role = u.Role;
            IsApproved = u.IsApproved;
            Balance = balance;
        }
    }
}