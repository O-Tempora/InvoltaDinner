using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DinnerContext _context;
        private DishRepos _dishRepository;
        private DishMenuRepos _dishMenuRepository;
        private RecordRepos _recordRepository;
        private RecordDishRepos _recordDishRepository;
        private DinnerMenuRepos _dinnerMenuRepository;

        public UnitOfWork()
        {
            _context = new DinnerContext();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public IRepository<Record> RecordRepository
        {
            get
            {
                if (_recordRepository == null)
                    _recordRepository = new RecordRepos(_context);
                return _recordRepository;
            }
        }
        public IRepository<DinnerMenu> DinnerMenuRepository
        {
            get
            {
                if (_dinnerMenuRepository == null)
                    _dinnerMenuRepository = new DinnerMenuRepos(_context);
                return _dinnerMenuRepository;
            }
        }
        public IRepository<DishMenu> DishMenuRepository
        {
            get
            {
                if (_dishMenuRepository == null)
                    _dishMenuRepository = new DishMenuRepos(_context);
                return _dishMenuRepository;
            }
        }
        public IRepository<Dish> DishRepository
        {
            get
            {
                if (_dishRepository == null)
                    _dishRepository = new DishRepos(_context);
                return _dishRepository;
            }
        }
        public IRepository<RecordDish> RecordDishRepository
        {
            get
            {
                if (_recordDishRepository == null)
                    _recordDishRepository = new RecordDishRepos(_context);
                return _recordDishRepository;
            }
        }
    }
}