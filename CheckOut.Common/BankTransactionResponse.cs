using System;
using System.Collections.Generic;
using System.Text;

namespace CheckOut.Common
{
    public class BankTransactionResponse
    {
        public Guid BankRef { get; set; }
        public string StatusCode { get; set; }
        public string ReasonCode { get; set; }
        public string AuthCode { get; set; }
    }
}