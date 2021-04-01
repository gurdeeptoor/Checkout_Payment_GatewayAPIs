using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebAPI
{
    public class TransactionRequest
    {
        public string TransactionRef { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public int ExpMonth { get; set; }

        [Required]
        public int ExpYear { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        [MaxLength(3)]
        public string CVV { get; set; }

        [MaxLength(50)]
        public string Address1 { get; set; }

        [MaxLength(50)]
        public string Address2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(2)]
        public string CountryCode { get; set; }
    }
}