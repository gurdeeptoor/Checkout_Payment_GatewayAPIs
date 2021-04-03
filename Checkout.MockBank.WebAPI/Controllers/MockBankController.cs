using CheckOut.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.MockBank.WebAPI.Controllers
{
    [ApiController]
    [Route("v1/BankTransactions")]
    public class MockBankController : ControllerBase
    {
        private readonly ILogger<MockBankController> _logger;

        public MockBankController(ILogger<MockBankController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Payment")]
        public async Task<IActionResult> PostBankTransaction([FromBody] BankTransactionRequest BankTransactionRequest)
        {

            BankTransactionStatusCode statusCode = BankTransactionStatusCode.Failed;
            string supplymentaryInfo = string.Empty;

            MockBankData MockBankData = new MockBankData();

            var CardDetails = MockBankData.MockBankCards().FirstOrDefault(x => x.CardNum == BankTransactionRequest.CardNum &&
                                                                          x.Cvv == BankTransactionRequest.Cvv &&
                                                                          x.ExpMonth == BankTransactionRequest.ExpMonth &&
                                                                          x.HolderName == BankTransactionRequest.HolderName &&
                                                                          x.ExpYear == BankTransactionRequest.ExpYear);

            if (CardDetails == null)           
                supplymentaryInfo = "Invalid Card Details";           
            else if (!CardDetails.IsActivated)            
                supplymentaryInfo = "Card not activated";          
            else if (CardDetails.ExpYear < DateTime.Now.Year || (CardDetails.ExpYear == DateTime.Now.Year && CardDetails.ExpMonth < DateTime.Now.Month))            
                supplymentaryInfo = "Card expired";          
            else if (CardDetails.RemainingBalance < BankTransactionRequest.Amount)  
                supplymentaryInfo = "Insufficent funds";            
            else
            {
                statusCode = BankTransactionStatusCode.Sucessful;
                //Internal Bank Payment processing goes here 
                CardDetails.RemainingBalance -= BankTransactionRequest.Amount;
                supplymentaryInfo = "Payment Sucessful";
            }

            var x = new BankTransactionRespose()
            {
                BankRef = Guid.NewGuid(),
                StatusCode = statusCode,
                SupplymentaryInfo = supplymentaryInfo
            };

            if (statusCode == BankTransactionStatusCode.Sucessful)
                return Created(string.Empty, x);
            else
                return BadRequest(x);
        }
    }
}