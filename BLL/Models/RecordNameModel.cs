using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class RecordNameModel
    {
        public string UserName { get; set; }
        public List<string> DishesList { get; set; }
        
        public RecordNameModel() { }
        public RecordNameModel(string username, List<string> dishes) { 
            this.UserName = username;
            this.DishesList = dishes;
        }
    }
}
