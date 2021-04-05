using Checkout.PaymentGateway.Data;
using CheckOut.Common;
using CreditCardValidator;
using System;
using System.Linq;

namespace Checkout.PaymentGateway.Core
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        private CheckOutDBContext _checkOutDBContext;
        public CurrencyRepository(CheckOutDBContext checkOutDBContext) : base(checkOutDBContext)
        {
            _checkOutDBContext = checkOutDBContext;
        }

        public bool IsCurrenyValid(string CurrencyCode)
        {
            //check if exp date & Year are correct
            var x = _checkOutDBContext.Currencies.Count(x => x.Code == CurrencyCode);
            return x > 0;
        }
    }
}