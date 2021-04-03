using System;
using System.Collections.Generic;
using System.Text;

namespace CheckOut.Common
{
    public class BankTransactionRequest
    {
        public string CardNum { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string HolderName { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        public string MerchantRef { get; set; }
    }
}