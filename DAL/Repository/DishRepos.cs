using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class DishRepos : IRepository<Dish>
    {
        private DinnerContext _context;
        public DishRepos(DinnerContext context)
        {
            _context = context;
        }
        public List<Dish> GetAll()
        {
            return _context.Dishes.ToList();
        }
        public Dish Get(int id)
        {
            return _context.Dishes.Find(id);
        }
        public void Create(Dish item)
        {
            _context.Dishes.Add(item);
        }
        public void Update(Dish item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Delete(int id)
        {
            Dish menuItem = _context.Dishes.Find(id);
            if(menuItem != null)
            {
                _context.Dishes.Remove(menuItem);
            }
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}