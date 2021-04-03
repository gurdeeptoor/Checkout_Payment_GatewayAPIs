using Checkout.PaymentGateway.Data;
using CreditCardValidator;
using System;

namespace Checkout.PaymentGatway.Core
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
            if (ExpYear < DateTime.Now.Year || (ExpYear == DateTime.Now.Year && ExpMonth < DateTime.Now.Month))
                return false;
             
            //Check if number is valid using NuGet
            CreditCardDetector detector = new CreditCardDetector(CardNumber);
            return detector.IsValid();
        }
    }
}