using NUnit.Framework;

namespace Checkout.PaymentGatway.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /* 
         1 - Authentication
           - Invalid Key, Valid Key, Blank Key, Other Merchant Key
         2 - POST Payment
           - Invalid, Valid
         3 - GET Payment
           - Invalid, Valid
         */
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}