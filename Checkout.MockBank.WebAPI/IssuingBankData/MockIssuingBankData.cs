using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.MockBank.WebAPI
{
    /// <summary>
    /// This is mock data held at Card Issuing Bank. The Aquiring Bank will check using the Issuing Bank network via Payment Scheme. 
    /// In Real life this will be done via Scheme system but for Mock purposes the static data is used
    /// </summary>
    public class MockIssuingBankData
    {
        public class BankCard 
        {
            public decimal RemainingBalance { get; set; }
            public string CardNum { get; set; }
            public int ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string HolderName { get; set; }
            public string Cvv { get; set; }
            public bool IsActivated { get; set; }

            //Address details ommitted for brevity 
        }

        /// <summary>
        /// Returns the Mock Data for Bank Cards held at Issuing Bank
        /// Surce - https://www.paypalobjects.com/en_GB/vhelp/paypalmanager_help/credit_card_numbers.htm
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BankCard> MockIssuingBankCards()
        {
            List<BankCard> BankCards = new List<BankCard>();

            BankCards.Add(new BankCard
            {
                //Active Card - Can be used for payments
                CardNum = "4111111111111111",
                Cvv = "122",
                ExpMonth = 12,
                ExpYear = 21,
                HolderName = "Mr Checkout 1",
                IsActivated = true,
                RemainingBalance = 1500
            });
            BankCards.Add(new BankCard
            {
                //Used Card - Over Credit Limit - below zero balance
                CardNum = "4012888888881881",
                Cvv = "222",
                ExpMonth = 6,
                ExpYear = 23,
                HolderName = "Mrs Checkout 2",
                IsActivated = true,
                RemainingBalance = (decimal)-10.50
            });
            BankCards.Add(new BankCard
            {
                //Starter Card - not activated yet (IsEabled = false)
                CardNum = "5555555555554444",
                Cvv = "322",
                ExpMonth = 1,
                ExpYear = 25,
                HolderName = "Miss Checkout 3",
                IsActivated = false,
                RemainingBalance = 1000
            });

            return BankCards.AsEnumerable();
        }
    }
}
