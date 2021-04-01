using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class Currency
    {
        public Currency()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
