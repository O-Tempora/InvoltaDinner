using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Record> RecordRepository {get; }
        IRepository<DinnerMenu> DinnerMenuRepository {get; }
        IRepository<DishMenu> DishMenuRepository {get; }
        IRepository<Dish> DishRepository {get; }
        IRepository<RecordDish> RecordDishRepository {get; }

        int Save();
    }
}