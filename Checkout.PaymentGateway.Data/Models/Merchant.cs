using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class Merchant
    {
        public Merchant()
        {
            MerchantApikeys = new HashSet<MerchantApikey>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public Guid MerchantRef { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<MerchantApikey> MerchantApikeys { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
