using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebAPI
{
    public class TransactionResponse
    { 
        public Guid TransactionRef { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string CardHolderName { get; set; }
        public string CVV { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }       
        public string Currency { get; set; }
        public string Status { get; set; }
        public string AuthCode { get; set; }
    }
}