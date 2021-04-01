using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class Bank
    {
        public Bank()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int BankId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
