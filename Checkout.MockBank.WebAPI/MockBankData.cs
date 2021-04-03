using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.MockBank.WebAPI
{
    public class MockBankData
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
        }

        /// <summary>
        /// Returns the Mock Data for Bank Cards
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BankCard> MockBankCards()
        {
            IEnumerable<BankCard> BankCards = Enumerable.Empty<BankCard>();

            BankCards.Append(new BankCard
            {
                //Active Card - Can be used for payments
                CardNum = "",
                Cvv = "122",
                ExpMonth = 12,
                ExpYear = 2021,
                HolderName = "Mr Checkout 1",
                IsActivated = true,
                RemainingBalance = 1500
            });
            BankCards.Append(new BankCard
            {
                //Used Card - Over Credit Limit - below zero balance
                CardNum = "",
                Cvv = "222",
                ExpMonth = 6,
                ExpYear = 2023,
                HolderName = "Mrs Checkout 2",
                IsActivated = true,
                RemainingBalance = (decimal)-10.50
            });
            BankCards.Append(new BankCard
            {
                //Starter Card - not activated yet (IsEabled = false)
                CardNum = "",
                Cvv = "322",
                ExpMonth = 1,
                ExpYear = 2025,
                HolderName = "Miss Checkout 3",
                IsActivated = false,
                RemainingBalance = 1000
            });

            return BankCards;
        }
    }
}
