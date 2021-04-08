using Checkout.PaymentGateway.Data;
using CheckOut.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private CheckOutDBContext checkOutDBContext; 
        private ITransactionRepository transaction;
        private ICardRepository cards;
        private IMerchantRepository merchants;
        private ICurrencyRepository currencies;
        private BankAPISettings bankAPISettings;
        public UnitOfWork(CheckOutDBContext CheckOutDBContext, IOptions<BankAPISettings> BankAPISettingsConfig)
        {
            checkOutDBContext = CheckOutDBContext;
            bankAPISettings = BankAPISettingsConfig.Value;
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

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) 
                {
                    checkOutDBContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            checkOutDBContext.SaveChanges();
        }
    }
}
