using Checkout.PaymentGateway.Data;
using CheckOut.Common;
using CreditCardValidator;
using System;

namespace Checkout.PaymentGateway.Core
{
    public class CardRepository : Repository<CardDetail>, ICardRepository
    {
        private CheckOutDBContext _checkOutDBContext;
        public CardRepository(CheckOutDBContext checkOutDBContext) : base(checkOutDBContext)
        {
            _checkOutDBContext = checkOutDBContext;
        }

        public bool IsCardValid(string CardNumber, int ExpMonth, int ExpYear)
        {
            //check if exp date & Year are correct
            if (ExpYear.To2DigitYear() < DateTime.Now.Year.To2DigitYear() || (ExpYear.To2DigitYear() == DateTime.Now.Year.To2DigitYear() && ExpMonth < DateTime.Now.Month))
                return false;

            if (!Int64.TryParse(CardNumber.Trim(), out var x))
                return false;
            //Check if number is valid using NuGet CreditCardValidator package
            CreditCardDetector detector = new CreditCardDetector(CardNumber);
            return detector.IsValid();
        }
    }
}