using System;
using System.Collections.Generic;

#nullable disable

namespace Checkout.PaymentGateway.Data
{
    public partial class TransactionHistory
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public int TransactionStatusId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
    }
}
