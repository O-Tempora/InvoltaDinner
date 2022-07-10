using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ChangeRoleModel
    {
        public int UserId {get;set;}
        public string Role {get;set;}
        public ChangeRoleModel() { }
        public ChangeRoleModel(int id, string role)
        {
            UserId = id;
            Role = role;
        }
    }
}