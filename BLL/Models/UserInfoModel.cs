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
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool IsApproved {get; set;}

        
        public UserInfoModel() { }
        public UserInfoModel(UserModel u) { 
            Id = u.Id;
            Email = u.Email;
            Name = u.Name;
            Role = u.Role;
            IsApproved = u.IsApproved;
        }
    }
}