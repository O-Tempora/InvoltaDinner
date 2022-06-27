using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class RecordDishRepos : IRepository<RecordDish>
    {
        private DinnerContext _context;
        public RecordDishRepos(DinnerContext context)
        {
            _context = context;
        }
        public List<RecordDish> GetAll()
        {
            return _context.RecordDishes.ToList();
        }
        public RecordDish Get(int id)
        {
            return _context.RecordDishes.Find(id);
        }
        public void Create(RecordDish item)
        {
            _context.RecordDishes.Add(item);
        }
        public void Update(RecordDish item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Delete(int id)
        {
            RecordDish menuItem = _context.RecordDishes.Find(id);
            if(menuItem != null)
            {
                _context.RecordDishes.Remove(menuItem);
            }
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}