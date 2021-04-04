using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.WebAPI
{
    public class TransactionRequest
    {
        public string TransactionRef { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        [Required]
        [MinLength(16)]
        [MaxLength(19)]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [Required]
        [Range(1, 12)]
        public int ExpMonth { get; set; }

        [Required]
        [Range(2021, 2099)]
        public int ExpYear { get; set; }

        [Required]        
        public string CardHolderName { get; set; }

        [Required]
        [MaxLength(3)]
        [MinLength(1)]
        public string CVV { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address1 { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(2)]
        [MinLength(2)]
        [Required]
        public string CountryCode { get; set; }
    }
}