using Microsoft.EntityFrameworkCore;
using SmartHouse.Api.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartHouse.Api.Services
{
    public interface IData<T> where T : class
    {
        IEnumerable<T> Get();
        List<T> GetByCondition(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
        T GetLastT();
    }

    public class Data<T> : IData<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entity;

        public Data(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            var obj = _entity.AsNoTracking().AsEnumerable();
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }
            return obj;
        }

        public List<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            var obj = _entity.Where(predicate).ToList();
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }
            return obj;
        }

        public T GetLastT()
        {
            var obj = _entity.ToList().LastOrDefault();
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }
            return obj;
        }

        public T Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Entity");
            }

            var x = _entity.Add(obj);
            _context.SaveChanges();
            return x.Entity;
        }
    }
}
