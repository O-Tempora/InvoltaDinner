using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class ChangeUserBalanceModel
    {
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public ChangeUserBalanceModel() { }
        public ChangeUserBalanceModel(int adminId, int userId, decimal price, DateTime date) 
        {
            AdminId = adminId;
            UserId = userId;
            Price = price;
            Date = date;
        }
    }
}