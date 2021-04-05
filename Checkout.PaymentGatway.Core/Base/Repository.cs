using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Checkout.PaymentGateway.Core
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext DBContext { get; set; } 

        public Repository(DbContext bBContext)
        {
            DBContext = bBContext;
        } 

        public IQueryable<T> FindAll()
        {
            return this.DBContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DBContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.DBContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.DBContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.DBContext.Set<T>().Remove(entity);
        }
    }
}