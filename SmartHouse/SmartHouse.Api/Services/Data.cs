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
            return _entity.AsNoTracking().AsEnumerable();
        }

        public List<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate).ToList();
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
