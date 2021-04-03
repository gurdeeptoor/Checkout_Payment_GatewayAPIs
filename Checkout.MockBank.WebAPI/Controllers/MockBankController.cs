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
        [Route("Add")]
        public async Task<IActionResult> PostBankTransaction([FromBody] BankTransactionRequest BankTransactionRequest)
        {

            BankTransactionStatusCode statusCode;
            string supplymentaryInfo = "OK";

            MockBankData MockBankData = new MockBankData();

            var CardDetails = MockBankData.MockBankCards().FirstOrDefault(x => x.CardNum == BankTransactionRequest.CardNum &&
                                                                          x.Cvv == BankTransactionRequest.Cvv &&
                                                                          x.ExpMonth == BankTransactionRequest.ExpMonth &&
                                                                          x.HolderName == BankTransactionRequest.HolderName &&
                                                                          x.ExpYear == BankTransactionRequest.ExpYear);

            if (CardDetails == null)
            {
                statusCode = BankTransactionStatusCode.Failed;
                supplymentaryInfo = "Invalid Card Details";
            }
            else if (!CardDetails.IsActivated)
            {
                statusCode = BankTransactionStatusCode.Failed;
                supplymentaryInfo = "Card not activated";
            }
            else if (CardDetails.ExpYear < DateTime.Now.Year || (CardDetails.ExpYear == DateTime.Now.Year && CardDetails.ExpMonth < DateTime.Now.Month))
            {
                statusCode = BankTransactionStatusCode.Failed;
                supplymentaryInfo = "Card expired";
            }
            else if (CardDetails.RemainingBalance < BankTransactionRequest.Amount)
            {
                statusCode = BankTransactionStatusCode.Failed;
                supplymentaryInfo = "Insuffiecnt funds";
            }
            else
            {
                //Process payment here
                statusCode = BankTransactionStatusCode.Sucessful;
                CardDetails.RemainingBalance -= BankTransactionRequest.Amount;
            }

            var x = new BankTransactionRespose()
            {
                BankRef = Guid.NewGuid(),
                StatusCode = statusCode,
                SupplymentaryInfo = supplymentaryInfo
            };

            if (statusCode == BankTransactionStatusCode.Sucessful)
                return Ok(x);
            else
                return BadRequest(x);
        }
    }
}