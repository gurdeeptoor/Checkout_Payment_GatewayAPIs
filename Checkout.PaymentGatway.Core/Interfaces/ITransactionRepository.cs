using Checkout.PaymentGateway.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Core
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Transaction GetTrasactionByRef(string RefNo);
        Task<Transaction> ProcessAquiringBankTrasactionAsync(Transaction Transaction);
        IEnumerable<Transaction> GetTrasactionsByMerchantRef(string MerchantRefNo);
    }
}