using Microsoft.EntityFrameworkCore;
using SmartHouse.Api.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartHouse.Api.Services
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
        T GetLastT();
    }

    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entity;

        public BaseService(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            var obj = _entity.AsNoTracking().AsEnumerable();
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate).AsEnumerable();
        }

        public T GetLastT()
        {
            var obj = _entity.ToList().LastOrDefault();
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        public T Insert(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            var x = _entity.Add(obj);
            _context.SaveChanges();
            return x.Entity;
        }
    }
}
