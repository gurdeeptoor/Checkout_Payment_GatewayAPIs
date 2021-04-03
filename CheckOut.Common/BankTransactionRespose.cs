using System;
using System.Collections.Generic;
using System.Text;

namespace CheckOut.Common
{
    public class BankTransactionRespose
    {
        public Guid BankRef { get; set; }
        public BankTransactionStatusCode StatusCode { get; set; }
        public string SupplymentaryInfo { get; set; }
    }
} 