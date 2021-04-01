using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            TransactionHistories = new HashSet<TransactionHistory>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
