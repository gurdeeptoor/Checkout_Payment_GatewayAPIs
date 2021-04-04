using System;
using System.Collections.Generic;
using System.Text;

namespace CheckOut.Common
{
    public enum TransactionStatusCode
    {
        Sucessful,
        Failed
    }

    public enum TransactionReasonCode
    {
        PaymentOK,
        InvalidCardDetails,
        CardNotActivated,
        CardExpired,
        InsufficentFnds,
        None
    }
}