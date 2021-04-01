using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class MerchantApikey
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string Apikey { get; set; }
        public bool IsEnabled { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
