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

    public enum TransactionStatusID : int
    {
        Pending = 1,
        Completed = 2,
        Cancelled = 3,
        Rejected = 4,
        Reversed = 5
    }
}