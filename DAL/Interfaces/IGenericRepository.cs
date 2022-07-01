using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? Get(int id);
        void Create(T entity);
        bool Delete(int id);
        void Update(T entityToUpdate);
        void Save();
    }
}