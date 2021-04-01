using Checkout.PaymentGateway.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGatway.Core
{
    public class TransactionValidation : AbstractValidator<Transaction>
    {
        public TransactionValidation()
        {
            //Basic Transaction validations
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Transaction amount must be greater than 0");
            RuleFor(x => x.Currency.Code).NotEmpty().WithMessage("Currency is required");

            //Card Validations
            RuleFor(x => x.CardDetail.CardNum).NotEmpty().WithMessage("Card Number is required");
            RuleFor(x => x.CardDetail.ExpMonth).NotEmpty().WithMessage("Card Exp Month is required");
            RuleFor(x => x.CardDetail.ExpYear).NotEmpty().WithMessage("Card Exp Year is required");
            RuleFor(x => x.CardDetail.HolderName).NotEmpty().WithMessage("Card Holder Name is required");
        }
    }
}