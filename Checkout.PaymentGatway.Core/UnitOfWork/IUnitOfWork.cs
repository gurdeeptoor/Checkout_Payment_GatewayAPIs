using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGatway.Core
{
    public interface IUnitOfWork
    {
        ITransactionRepository Transactions { get; }
        ICardRepository Cards { get; }
        IMerchantRepository Merchants { get; }
        ICurrencyRepository Currencies { get; }
        void Save();
    }
}