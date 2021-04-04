using Checkout.PaymentGateway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Transaction GetTrasactionByRef(string RefNo)
        {
            if (Guid.TryParse(RefNo, out var uRefNo))
                return _checkOutDBContext.Transactions.FirstOrDefault(x => x.BankRef == uRefNo);
            return null;
        }
    }
}