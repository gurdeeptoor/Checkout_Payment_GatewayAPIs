﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Checkout.PaymentGateway.Core
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}