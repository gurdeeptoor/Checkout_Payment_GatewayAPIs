using Checkout.PaymentGateway.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Core
{
    public interface IMerchantRepository : IRepository<Merchant>
    {
        Merchant GetMerchantByKey(string MerchantAPIKey);
    }   
}