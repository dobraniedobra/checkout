using System;
using System.Linq;
using System.Linq.Expressions;

namespace Checkout.TechnicalTest.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> GetWithIncludes(Expression<Func<T, object>>[] includes);
        IQueryable<T> GetConditional(Expression<Func<T, bool>> query);
        IQueryable<T> GetConditionalWithIncludes(Expression<Func<T, bool>> query, Expression<Func<T, object>>[] includes);
        T GetById(int id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
