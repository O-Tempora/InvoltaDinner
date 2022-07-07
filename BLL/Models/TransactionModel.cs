using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Admin { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        
        public TransactionModel() { }
        public TransactionModel(Transaction r) { 
            Id = r.Id;
            User = r.User;
            Admin = (int)r.Admin;
            Price = r.Price;
            Date = r.Date;
        }
    }
}