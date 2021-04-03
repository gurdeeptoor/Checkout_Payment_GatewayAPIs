using Checkout.PaymentGateway.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGatway.Core
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
       private CheckOutDBContext _checkOutDBContext;
        public TransactionRepository(CheckOutDBContext checkOutDBContext) : base(checkOutDBContext)
        {
            _checkOutDBContext = checkOutDBContext;
        }
    }
}