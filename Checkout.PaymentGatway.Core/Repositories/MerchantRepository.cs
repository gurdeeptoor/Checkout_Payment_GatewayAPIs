using Checkout.PaymentGateway.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Core
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        private CheckOutDBContext _checkOutDBContext;

        public MerchantRepository(CheckOutDBContext checkOutDBContext) : base(checkOutDBContext)
        {
            _checkOutDBContext = checkOutDBContext;
        }

        public Merchant GetMerchantByKey(string MerchantAPIKey)
        {
            if (!(string.IsNullOrEmpty(MerchantAPIKey)))
            {
                var merchantKeys = _checkOutDBContext.MerchantApikeys.Include("Merchant").Where(x => x.Apikey == MerchantAPIKey && x.IsEnabled == true).FirstOrDefault();

                if (merchantKeys != null)
                    return merchantKeys.Merchant;

            }
            return null;
        }
    }
}