using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class RecordPosModel
    {
        public DateTime Date { get; set; }
        public int position { get; set; }
        
        public RecordPosModel() { }
        public RecordPosModel(DateTime date, int position) { 
            this.Date = date;
            this.position = position;
        }
    }
}