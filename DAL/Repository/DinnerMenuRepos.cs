using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class DinnerMenuRepos : IRepository<DinnerMenu>
    {
        private DinnerContext _context;
        public DinnerMenuRepos(DinnerContext context)
        {
            _context = context;
        }
        public List<DinnerMenu> GetAll()
        {
            return _context.DinnerMenus.ToList();
        }
        public DinnerMenu Get(int id)
        {
            return _context.DinnerMenus.Find(id);
        }
        public void Create(DinnerMenu item)
        {
            _context.DinnerMenus.Add(item);
        }
        public void Update(DinnerMenu item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Delete(int id)
        {
            DinnerMenu menuItem = _context.DinnerMenus.Find(id);
            if(menuItem != null)
            {
                _context.DinnerMenus.Remove(menuItem);
            }
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}