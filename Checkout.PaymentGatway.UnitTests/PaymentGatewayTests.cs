using Autofac;
using Autofac.Extras.Moq;
using Checkout.PaymentGateway.Core;
using Checkout.PaymentGateway.Data;
using Checkout.PaymentGateway.WebAPI.Controllers;
using CheckOut.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Checkout.PaymentGatway.UnitTests
{
    public class PaymentGatewayTests
    {
        readonly Guid TxnGuid = Guid.NewGuid();
        readonly Guid MerchantGuId = Guid.NewGuid();
        readonly Guid BankGuId = Guid.NewGuid();

        #region "Test Data"

        public TransactionRequest ValidInputTransaction()
        {
            var ValidCard = TestCards().FirstOrDefault();
            var ValidCurrency = TestCurrencies().FirstOrDefault();

            return new TransactionRequest
            {
                CardNumber = ValidCard.CardNum,
                Address1 = ValidCard.Address1,
                Address2 = ValidCard.Address2,
                Amount = 2,
                CardHolderName = ValidCard.HolderName,
                City = ValidCard.City,
                CountryCode = ValidCard.CountryCode,
                CurrencyCode = ValidCurrency.Code,
                CVV = ValidCard.Cvv,
                ExpMonth = ValidCard.ExpMonth,
                ExpYear = ValidCard.ExpYear,
                State = ValidCard.State,
                TransactionRef = "Test1"
            };
        }

        public IQueryable<CardDetail> TestCards()
        {
            List<CardDetail> x = new List<CardDetail>();
            x.Add(new CardDetail { Cvv = "122", CardNum = "4111111111111111", ExpMonth = 1, ExpYear = 23, IsEnabled = true });
            return x.AsQueryable();
        }

        public IQueryable<Merchant> TestMerchants()
        {
            List<Merchant> x = new List<Merchant>();

            x.Add(new Merchant
            {
                Id = 1,
                IsEnabled = true,
                MerchantRef = MerchantGuId,
                Name = "Merchant-01",
                MerchantApikeys = TestMerchantKeys().Where(x => x.MerchantId == 1).ToList()
            });

            return x.AsQueryable();
        }

        public IQueryable<MerchantApikey> TestMerchantKeys()
        {
            List<MerchantApikey> x = new List<MerchantApikey>();
            x.Add(new MerchantApikey { Apikey = "APIKEY001", Id = 1, IsEnabled = true, MerchantId = 1 });
            return x.AsQueryable();
        }

        public IQueryable<Currency> TestCurrencies()
        {
            List<Currency> x = new List<Currency>();
            x.Add(new Currency { Code = "USD", Id = 1, IsEnabled = true, Name = "US Dollar", Symbol = "$" });
            x.Add(new Currency { Code = "GBP", Id = 1, IsEnabled = true, Name = "Pounds", Symbol = "£" });
            return x.AsQueryable();
        }

        public IQueryable<TransactionStatus> TestTransactionStaus()
        {
            List<TransactionStatus> x = new List<TransactionStatus>();
            x.Add(new TransactionStatus { Id = 1, StatusName = "Pending" });
            x.Add(new TransactionStatus { Id = 2, StatusName = "Posted" });
            return x.AsQueryable();
        }

        public IQueryable<Transaction> TestTransaction()
        {
            List<Transaction> x = new List<Transaction>();

            x.Add(new Transaction
            {
                Amount = 10,
                CardDetail = TestCards().FirstOrDefault(x => x.Id == 1),
                Currency = TestCurrencies().FirstOrDefault(x => x.Id == 1),
                Merchant = TestMerchants().FirstOrDefault(x => x.Id == 1),
                AuthCode = "XXX",
                BankRef = BankGuId,
                CardDetailId = 1,
                TransactionStatusId = 2,
                TransactionId = TxnGuid,
                CreatedDate = DateTime.Now,
                MerchantRef = "MM001",
                MerchantId = 1,
                CurrencyId = 1,
                SourceType = ""
            });

            return x.AsQueryable();
        }

        #endregion

        #region "Transaction Tests"

        [Fact]
        public void Get_Transaction_By_Valid_Guid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();
                uowMock.Setup(u => u.Transactions.GetTrasactionByRef(It.IsAny<string>())).Returns(TestTransaction().FirstOrDefault(x => x.TransactionId == TxnGuid));

                var actual = uowMock.Object.Transactions.GetTrasactionByRef(TxnGuid.ToString());
                var expected = TestTransaction().FirstOrDefault();

                Assert.NotNull(actual);
                Assert.Equal(actual.TransactionId, expected.TransactionId);
            }
        }

        [Theory]
        [InlineData("Invalid")] //Invalid string (Not GUID)
        [InlineData("")] //Blank String
        [InlineData(("DFB4F7B4-C487-4188-8AC6-A7E5170B1A2B"))] //Invalid GUID
        public void Get_Transaction_By_Null_String(string TransactionId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();

                uowMock.Setup(u => u.Transactions.GetTrasactionByRef(TransactionId.ToString())).Returns(TestTransaction().FirstOrDefault(x => x.TransactionId.ToString() == TransactionId));

                var actual = uowMock.Object.Transactions.GetTrasactionByRef(TransactionId);

                Assert.Null(actual);
            }
        }

        [Fact]
        public void Get_Merchant_Transactions_By_Valid_MerchantID()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();
                var MerchantTxns = TestTransaction().Where(x => x.Merchant.MerchantRef == MerchantGuId);

                uowMock.Setup(u => u.Transactions.GetTrasactionsByMerchantRef(It.IsAny<string>())).Returns(MerchantTxns);

                var actual = uowMock.Object.Transactions.GetTrasactionsByMerchantRef(MerchantGuId.ToString());

                Assert.NotNull(actual);
                Assert.Equal(actual.Count(), MerchantTxns.Count());
            }
        }

        [Theory]
        [InlineData("Invalid")] //Invalid string (Not GUID)
        [InlineData("")] //Blank String
        [InlineData(("DFB4F7B4-C487-4188-8AC6-A7E5170B1A2B"))] //Invalid GUID
        public void Get_Merchant_Transactions_By_Invalid_MerchantID(string MerchantId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();
                var MerchantTxns = TestTransaction().Where(x => x.Merchant.MerchantRef.ToString() == MerchantId);

                uowMock.Setup(u => u.Transactions.GetTrasactionsByMerchantRef(It.IsAny<string>())).Returns(MerchantTxns);

                var actual = uowMock.Object.Transactions.GetTrasactionsByMerchantRef(MerchantId);

                Assert.Equal(actual.Count(), MerchantTxns.Count());
            }
        }

        #endregion

        #region "Card Tests"

        [Theory]
        [InlineData("4111111111111111", 1, 23)] //Valid Card
        public void Test_Card_Valid(string CardNumber, int ExpMonth, int ExpYear)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();
                var CardRepo = mock.Create<CardRepository>();

                uowMock.Setup(u => u.Cards.IsCardValid(CardNumber, ExpMonth, ExpYear)).Returns(CardRepo.IsCardValid(CardNumber, ExpMonth, ExpYear));

                var actual = uowMock.Object.Cards.IsCardValid(CardNumber, ExpMonth, ExpYear);

                Assert.True(actual);
            }
        }

        [Theory]
        [InlineData("Invalid", 1, 23)] //Invalid Card Number
        [InlineData("91111001111110", 1, 23)] //Invalid Card Number
        [InlineData("", 1, 23)] //Bank Card Number
        [InlineData("4111111111111111", 13, 23)] //Invalid Month Number
        [InlineData("4111111111111111", 1, 19)] //Invalid Year Number (Past Year)
        public void Test_Invalid_Card_Valid(string CardNumber, int ExpMonth, int ExpYear)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();
                var CardRepo = mock.Create<CardRepository>();

                uowMock.Setup(u => u.Cards.IsCardValid(CardNumber, ExpMonth, ExpYear)).Returns(CardRepo.IsCardValid(CardNumber, ExpMonth, ExpYear));

                var actual = uowMock.Object.Cards.IsCardValid(CardNumber, ExpMonth, ExpYear);

                Assert.False(actual);
            }
        }

        #endregion

        #region "Currency Tests"

        [Theory]
        [InlineData("USD")] //Valid Data
        [InlineData("GBP")] //Valid Data
        public void Test_Currency_Valid(string CurrencyCode) 
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();

                uowMock.Setup(u => u.Currencies.IsCurrenyValid(CurrencyCode)).Returns(TestCurrencies().Count(x => x.Code == CurrencyCode) > 0);

                var actual = uowMock.Object.Currencies.IsCurrenyValid(CurrencyCode);

                Assert.True(actual);
            }
        }

        [Theory]
        [InlineData("INR")] //InValid Data
        [InlineData("")] //InValid Data
        public void Test_Currency_InValid(string CurrencyCode)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();

                uowMock.Setup(u => u.Currencies.IsCurrenyValid(CurrencyCode)).Returns(TestCurrencies().Count(x => x.Code == CurrencyCode) > 0);

                var actual = uowMock.Object.Currencies.IsCurrenyValid(CurrencyCode);

                Assert.False(actual);
            }
        }

        #endregion

        #region "Merchant APIKey Tests"

        [Theory]
        [InlineData("APIKEY001")] //Valid Data 
        public void Test_Merchant_Key_Valid(string MerchantAPIKey) 
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();
                var mKey = TestMerchantKeys().FirstOrDefault(x => x.Apikey == MerchantAPIKey);
                var merchant = TestMerchants().FirstOrDefault(x => x.Id == mKey.MerchantId);

                uowMock.Setup(u => u.Merchants.GetMerchantByKey(MerchantAPIKey)).Returns(merchant);

                var actual = uowMock.Object.Merchants.GetMerchantByKey(MerchantAPIKey);

                Assert.NotNull(actual);
                Assert.Equal(actual.Id, merchant.Id);
            }
        }

        [Theory]
        [InlineData("")] //Valid Data 
        [InlineData("xx")] //Valid Data 
        [InlineData(null)] //Valid Data 
        public void Test_Merchant_Key_InValid(string MerchantAPIKey)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var uowMock = mock.Mock<IUnitOfWork>();
                var mKey = TestMerchantKeys().FirstOrDefault(x => x.Apikey == MerchantAPIKey);
                var merchant = mKey == null ? null : TestMerchants().FirstOrDefault(x => x.Id == mKey.MerchantId);

                uowMock.Setup(u => u.Merchants.GetMerchantByKey(MerchantAPIKey)).Returns(merchant);

                var actual = uowMock.Object.Merchants.GetMerchantByKey(MerchantAPIKey);

                Assert.Null(actual);
            }
        }

        #endregion

        #region "Transaction Contoller Tests"

        [Fact(Skip ="Not Completed Yet")]        
        public void Test_Transaction_Controller_Post_Valid_Transaction()
        {
            //var transactionRequest = ValidInputTransaction();

            //using (var mock = AutoMock.GetLoose())
            //{
            //    var uowMock = mock.Mock<IUnitOfWork>();
            //    var loggerMock = mock.Create<ILogger>();
            //    var mKey = TestMerchantKeys().FirstOrDefault().Apikey;

            //    uowMock.Setup(u => u.Transactions.GetTrasactionByRef(It.IsAny<string>())).Returns(TestTransaction().FirstOrDefault(x => x.TransactionId == TxnGuid));
            //    //set up repos
            //    uowMock.Setup(u => u.Merchants.GetMerchantByKey(MerchantAPIKey)).Returns(merchant);

            //    var transactionController = new TransactionController((ILogger<TransactionController>)loggerMock, (IUnitOfWork)uowMock);

            //    var actual = transactionController.PostTransaction(transactionRequest);

            //    Assert.Null(actual);              
            //}
        }

        #endregion
    }
}