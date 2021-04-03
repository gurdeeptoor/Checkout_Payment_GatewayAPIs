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

        string visaNumber = 4953089013607
        string amexNumber => 373485467448025
        string masterCardNumber => 5201294442453002
        string chinaUnionPayNumber => 6280209982074556

         */
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}