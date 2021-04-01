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
