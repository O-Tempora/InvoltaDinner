using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class PeriodModel
    {
        public DateTime DateFirst { get; set; }
        public DateTime DateSecond { get; set; }

        public PeriodModel() { }
        public PeriodModel(DateTime dateFirst, DateTime dateSecond) 
        {
            DateFirst = dateFirst;
            DateSecond = dateSecond;
        }
    }
}