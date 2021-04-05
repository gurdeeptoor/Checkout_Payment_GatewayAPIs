using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Core
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