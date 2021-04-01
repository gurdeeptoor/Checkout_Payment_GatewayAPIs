using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public Guid TransactionId { get; set; }
        public int MerchantId { get; set; }
        public string MerchantRef { get; set; }
        public int CardDetailId { get; set; }
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TransactionStatusId { get; set; }
        public string AuthCode { get; set; }
        public Guid? BankRef { get; set; }
        public string SourceType { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual CardDetail CardDetail { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
