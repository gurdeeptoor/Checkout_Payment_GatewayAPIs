using Checkout.PaymentGateway.Data;
using CheckOut.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.WebAPI
{
    public static class TransactionExtensions
    {
        public static TransactionResponse ToTransactionResponse(this Transaction transaction)
        {
            return new TransactionResponse
            {
                Address1 = string.Empty,
                Address2 = string.Empty,
                Amount = transaction.Amount,
                AuthCode = transaction.AuthCode,
                CardHolderName = transaction.CardDetail.HolderName,
                CardNumber = transaction.CardDetail.CardNum.ToMaskedCardNumber(),
                City = string.Empty,
                Country = string.Empty,
                Currency = transaction.Currency.Code,
                CVV = transaction.CardDetail.Cvv,
                ExpMonth = transaction.CardDetail.ExpMonth,
                ExpYear = transaction.CardDetail.ExpYear,
                State = string.Empty,
                Status = Enum.GetName(typeof(TransactionStatusID), transaction.TransactionStatusId),
                TransactionRef = Guid.Parse(transaction.TransactionId.ToString())
            };
        }
    }
}