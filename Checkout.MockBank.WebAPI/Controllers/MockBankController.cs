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

        [HttpPost]
        [Route("Payment")]
        public async Task<IActionResult> PostBankTransaction([FromBody] BankTransactionRequest BankTransactionRequest)
        {

            TransactionStatusCode statusCode = TransactionStatusCode.Failed;
            TransactionReasonCode reasonCode = TransactionReasonCode.None;
            string authCode = string.Empty;

            MockIssuingBankData MockBankData = new MockIssuingBankData();

            //Card Basic validations ommitted for brevity as for this solution the validation are done at Payment Gateway

            //Call Scheme system and get data from Issuing Bank
            var CardDetails = MockBankData.MockIssuingBankCards().FirstOrDefault(x => x.CardNum == BankTransactionRequest.CardNum &&
                                                                          x.Cvv == BankTransactionRequest.Cvv &&
                                                                          x.ExpMonth == BankTransactionRequest.ExpMonth &&
                                                                          x.HolderName == BankTransactionRequest.HolderName &&
                                                                          x.ExpYear == BankTransactionRequest.ExpYear.To2DigitYear());

            if (CardDetails == null)
                reasonCode = TransactionReasonCode.InvalidCardDetails;
            else if (!CardDetails.IsActivated)
                reasonCode = TransactionReasonCode.CardNotActivated;
            else if (CardDetails.ExpYear.To2DigitYear() < DateTime.Now.Year.To2DigitYear() || (CardDetails.ExpYear.To2DigitYear() == DateTime.Now.Year.To2DigitYear() && CardDetails.ExpMonth < DateTime.Now.Month))
                reasonCode = TransactionReasonCode.CardExpired;
            else if (CardDetails.RemainingBalance < BankTransactionRequest.Amount)
                reasonCode = TransactionReasonCode.InsufficentFnds;
            else
            {
                statusCode = TransactionStatusCode.Sucessful;
                //Other internal Bank Payment processing logic goes here 
                CardDetails.RemainingBalance -= BankTransactionRequest.Amount;
                reasonCode = TransactionReasonCode.PaymentOK;
                authCode = "AUTH001";
            }

            var bankResponse = new BankTransactionResponse()
            {
                BankRef = Guid.NewGuid(),
                StatusCode = statusCode.ToString(),
                ReasonCode = reasonCode.ToString(),
               AuthCode = authCode
            };

            if (statusCode == TransactionStatusCode.Sucessful)
                return Created(string.Empty, bankResponse);
            else
                return BadRequest(bankResponse);
        }
    }
}