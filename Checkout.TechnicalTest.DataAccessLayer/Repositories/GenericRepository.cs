using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Checkout.TechnicalTest.DataAccessLayer.Context;
using Checkout.TechnicalTest.DataAccessLayer.Interfaces;

namespace Checkout.TechnicalTest.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _dataBaseContext;
        private readonly IDbSet<T> _dbSet;

        public GenericRepository(DatabaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            _dbSet = _dataBaseContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _dataBaseContext.Entry(entity).State = EntityState.Added;

        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);

            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (_dataBaseContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual IQueryable<T> Get()
        {
            return _dbSet;

        }

        public virtual IQueryable<T> GetWithIncludes(Expression<Func<T, object>>[] includes)
        {
            if (includes == null) return _dbSet;

            var dbSet = _dbSet;
            foreach (var include in includes)
            {
                dbSet.Include(include);
            }

            return dbSet;
        }

        public virtual IQueryable<T> GetConditional(Expression<Func<T, bool>> query)
        {
            return _dbSet.Where(query);
        }

        public virtual IQueryable<T> GetConditionalWithIncludes(Expression<Func<T, bool>> query, Expression<Func<T, object>>[] includes)
        {

            if (query == null) return _dbSet;
            var dbSet = _dbSet.Where(query);
            if (includes == null) return dbSet;
            foreach (var include in includes)
            {
                dbSet.Include(include);
            }
            return dbSet;
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Save()
        {
            _dataBaseContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataBaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
