using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Data;

namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Record> RecordRepository {get; }
        IGenericRepository<Dish> DishRepository {get; }
        IGenericRepository<User> UserRepository {get; }
        IGenericRepository<Menu> MenuRepository {get; }
        IGenericRepository<Transaction> TransactionRepository {get; }
        IGenericRepository<MenuDish> MenuDishRepository {get; }
        Task Save();
    }
}