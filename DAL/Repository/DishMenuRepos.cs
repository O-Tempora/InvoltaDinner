using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class DishMenuRepos : IRepository<DishMenu>
    {
        private DinnerContext _context;
        public DishMenuRepos(DinnerContext context)
        {
            _context = context;
        }
        public List<DishMenu> GetAll()
        {
            return _context.DishMenus.ToList();
        }
        public DishMenu Get(int id)
        {
            return _context.DishMenus.Find(id);
        }
        public void Create(DishMenu item)
        {
            _context.DishMenus.Add(item);
        }
        public void Update(DishMenu item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Delete(int id)
        {
            DishMenu menuItem = _context.DishMenus.Find(id);
            if(menuItem != null)
            {
                _context.DishMenus.Remove(menuItem);
            }
            Save();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}