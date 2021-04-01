using Checkout.PaymentGateway.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGatway.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private CheckOutDBContext checkOutDBContext; 
        private ITransactionRepository transaction;      

        public UnitOfWork(CheckOutDBContext CheckOutDBContext)
        {
            checkOutDBContext = CheckOutDBContext;
        }

        public ITransactionRepository Transactions
        {
            get
            {
                if (transaction == null) transaction = new TransactionRepository(checkOutDBContext);
                return transaction;
            }
        }

        public void Save()
        {
            checkOutDBContext.SaveChanges();
        }
    }
}
