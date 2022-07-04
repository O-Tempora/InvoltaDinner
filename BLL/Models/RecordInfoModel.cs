using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class RecordInfoModel
    {
        public int userId { get; set; }
        public DateTime date { get; set; }
        public int position { get; set; }
        
        public RecordInfoModel() { }
        public RecordInfoModel(DateTime date, int userId, int position) { 
            this.userId = userId;
            this.position = position;
            this.date = date;
        }
    }
}