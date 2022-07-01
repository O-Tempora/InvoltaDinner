using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL.Data;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal dinnerContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(dinnerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual bool Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);

            if(entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}