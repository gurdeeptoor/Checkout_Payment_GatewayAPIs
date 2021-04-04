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
        private ICardRepository cards;
        private IMerchantRepository merchants;
        private ICurrencyRepository currencies;
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

        public ICardRepository Cards
        {
            get
            {
                if (cards == null) cards = new CardRepository(checkOutDBContext);
                return cards;
            }
        }


        public IMerchantRepository Merchants
        {
            get
            {
                if (merchants == null) merchants = new MerchantRepository(checkOutDBContext);
                return merchants;
            }
        }

        public ICurrencyRepository Currencies
        {
            get
            {
                if (currencies == null) currencies = new CurrencyRepository(checkOutDBContext);
                return currencies;
            }
        }

        public void Save()
        {
            checkOutDBContext.SaveChanges();
        }
    }
}
