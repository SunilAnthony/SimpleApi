using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Interfaces;

namespace SimpleApi.Repositories
{
    public class Repository<T>: IRepository<T> where T: class, new()
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> entities;
       
        public Repository(DataContext _context)
        {
            this._context = _context;
            entities = _context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }
        public virtual T GetById(object id)
        {
            var item = entities.Find(id);
            if(item is null)
                return new T();
            return item;
        }
        public virtual void Insert(T obj)
        {
            entities.Add(obj);
        }
        public virtual void Update(T obj)
        {
            entities.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public virtual void Delete(object id)
        {
            var existing = entities.Find(id);
            if(existing is not null)
                entities.Remove(existing);
        }
        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}