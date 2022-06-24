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
        private RecordRepos _recordRepository;
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
    }
}