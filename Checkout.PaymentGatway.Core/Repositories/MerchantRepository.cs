using Checkout.PaymentGateway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGatway.Core
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        CheckOutDBContext _checkOutDBContext;

        public MerchantRepository(CheckOutDBContext checkOutDBContext) : base(checkOutDBContext)
        {
            _checkOutDBContext = checkOutDBContext;
        }

        public Merchant GetMerchantByKey(string MerchantAPIKey)
        {
            if (!(string.IsNullOrEmpty(MerchantAPIKey)))
            {
                var xx = _checkOutDBContext.MerchantApikeys.Where(x => x.Apikey == MerchantAPIKey && x.IsEnabled == true).FirstOrDefault();

                if (xx != null)
                    return _checkOutDBContext.Merchants.Where(x => x.Id == xx.MerchantId).FirstOrDefault();

            }
            return null;
        }
    }
}