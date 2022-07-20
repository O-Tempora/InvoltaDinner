using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserDataModel
    {
        public string UserName {get;set;}
        public int Id {get;set;}
        public string Role {get;set;}
        public sbyte IsApproved{get;set;}
        public UserDataModel() {}
    }
}