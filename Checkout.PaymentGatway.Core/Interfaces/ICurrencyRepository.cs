using Checkout.PaymentGateway.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Core
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        bool IsCurrenyValid(string CurrencyCode);
    }
}