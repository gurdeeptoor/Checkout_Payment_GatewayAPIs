using Checkout.PaymentGateway.Data;
using CheckOut.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private CheckOutDBContext checkOutDBContext; 
        private ITransactionRepository transaction;
        private ICardRepository cards;
        private IMerchantRepository merchants;
        private ICurrencyRepository currencies;
        private BankAPISettings bankAPISettings;
        public UnitOfWork(CheckOutDBContext CheckOutDBContext, BankAPISettings BankAPISettings)
        {
            checkOutDBContext = CheckOutDBContext;
            bankAPISettings = BankAPISettings;
        }

        public ITransactionRepository Transactions
        {
            get
            {
                if (transaction == null) transaction = new TransactionRepository(checkOutDBContext, bankAPISettings);
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
