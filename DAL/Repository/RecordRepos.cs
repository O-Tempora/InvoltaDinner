using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repository
{
    public class RecordRepos : IRepository<Record>
    {
        private DinnerContext _context;
        public RecordRepos(DinnerContext context)
        {
            _context = context;
        }
        public List<Record> GetAll()
        {
            return _context.Records.ToList();
        }
        public Record Get(int id)
        {
            return _context.Records.Find(id);
        }
        public void Create(Record item)
        {
            _context.Records.Add(item);
        }
        public void Update(Record item)
        {
            _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Delete(int id)
        {
            Record menuItem = _context.Records.Find(id);
            if(menuItem != null)
            {
                _context.Records.Remove(menuItem);
            }
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}