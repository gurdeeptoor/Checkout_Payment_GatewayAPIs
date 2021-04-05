using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class Bank
    {
        public int BankId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
