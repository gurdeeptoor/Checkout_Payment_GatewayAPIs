using Checkout.PaymentGatway.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("v1/Transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private IUnitOfWork _unitOfWork;

        public TransactionController(ILogger<TransactionController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionRequest transactionRequest)
        {
            //Validate Transaction request  
            if (transactionRequest == null || !ModelState.IsValid)
                return BadRequest("Invalid Transaction Parameters");

            //Validate Merchant
            var MerchantAPIKey = "57Dw2tFq9wF6"; //TODO - Get this from Auth Handler based on the API Key

            var Merchant = _unitOfWork.Merchants.GetMerchantByKey(MerchantAPIKey);
            if (Merchant == null)
                return BadRequest("Invalid Merchant details");

            //Validate Card
            if (!_unitOfWork.Cards.IsCardValid(transactionRequest.CardNumber, transactionRequest.ExpMonth, transactionRequest.ExpYear))
                return BadRequest("Invalid Card details");

            //Validate Currency
            if (!_unitOfWork.Currencies.IsCurrenyValid(transactionRequest.CurrencyCode))
                return BadRequest("Invalid Currency");

            //Process Bank Transactions
            //Make Bank API Call via Repository

            return Ok();
        }

        [HttpGet]
        [Route("transactionRef")]
        public async Task<IActionResult> GetTransaction([FromRoute] Guid transactionRef)
        {
            return Ok();
        }
    }
}
