using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class CardDetail
    {
        public CardDetail()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string CardNum { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string HolderName { get; set; }
        public string Cvv { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
