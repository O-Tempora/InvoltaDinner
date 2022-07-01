using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repository;

namespace BLL.Models
{
    public class UnitOfWork : BLL.Interfaces.IUnitOfWork
    {
        private readonly dinnerContext _context;
        private IGenericRepository<Record> _recordRep;
        private IGenericRepository<Dish> _dishRep;
        private IGenericRepository<User> _userRep;
        private IGenericRepository<Menu> _menuRep;
        private IGenericRepository<Transaction> _transactionRep;
        private IGenericRepository<MenuDish> _menuDishRep;
        public UnitOfWork(dinnerContext context)
        {
            _context = context;
        }
        public IGenericRepository<Record> RecordRepository => _recordRep ??= new GenericRepository<Record>(_context);
        public IGenericRepository<Dish> DishRepository => _dishRep ??= new GenericRepository<Dish>(_context);
        public IGenericRepository<User> UserRepository => _userRep ??= new GenericRepository<User>(_context);
        public IGenericRepository<Menu> MenuRepository => _menuRep ??= new GenericRepository<Menu>(_context);
        public IGenericRepository<Transaction> TransactionRepository => _transactionRep ??= new GenericRepository<Transaction>(_context);
        public IGenericRepository<MenuDish> MenuDishRepository => _menuDishRep ??= new GenericRepository<MenuDish>(_context);
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}