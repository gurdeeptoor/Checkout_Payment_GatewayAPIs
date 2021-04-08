using Checkout.PaymentGateway.Core;
using Checkout.PaymentGateway.Data;
using CheckOut.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

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
        public async Task<IActionResult> PostTransaction([FromBody] TransactionRequest transactionRequest)
        {
            ;
            //Validate Transaction request  
            if (transactionRequest == null || !ModelState.IsValid)
                return BadRequest("Invalid Transaction Parameters");

            //Get authenticated Merchant Ref from Identity Claims
            var AuthenticatedMerchantRef =  User?.Identities?.First().Claims?.First().Value;

            if (string.IsNullOrEmpty(AuthenticatedMerchantRef) || !Guid.TryParse(AuthenticatedMerchantRef, out var MerchantRefGUID))
                return BadRequest("Invalid Merchant details");

            var Merchant = _unitOfWork.Merchants.FindByCondition(x => x.MerchantRef == MerchantRefGUID && x.IsEnabled == true).FirstOrDefault();
            if (Merchant == null)
                return BadRequest("Invalid Merchant details");

            //Validate Card
            if (!_unitOfWork.Cards.IsCardValid(transactionRequest.CardNumber, transactionRequest.ExpMonth, transactionRequest.ExpYear))
                return BadRequest("Invalid Card details");

            //Validate Currency
            var currency = _unitOfWork.Currencies.FindByCondition(x => x.Code == transactionRequest.CurrencyCode.Trim() && x.IsEnabled == true).FirstOrDefault();

            if (currency == null)
                return BadRequest("Invalid Currency");

            //Save Card Details
            var CardDetails = _unitOfWork.Cards.FindByCondition(x => x.CardNum == transactionRequest.CardNumber.Trim()).FirstOrDefault();
            if (CardDetails == null)
            {
                _logger.LogInformation("Card does not exist so adding new one");

                var NewCard = new CardDetail
                {
                    CardNum = transactionRequest.CardNumber,
                    Cvv = transactionRequest.CVV,
                    ExpMonth = transactionRequest.ExpMonth,
                    ExpYear = transactionRequest.ExpYear,
                    HolderName = transactionRequest.CardHolderName,
                    IsEnabled = true
                };

                _unitOfWork.Cards.Create(NewCard);
                _unitOfWork.Save();
                CardDetails = NewCard;
            }
            else
            {
                _logger.LogInformation("Card exists");

                //validate if the supplied Name & details matches with previously saved Card
                if ((CardDetails.HolderName != transactionRequest.CardHolderName))
                    return BadRequest("Invalid Card details");                 
            }

            //Process Bank Transactions
            var Trans = new Transaction
            {
                Amount = transactionRequest.Amount,
                CardDetailId = CardDetails.Id,
                CreatedDate = DateTime.Now,
                CurrencyId = currency.Id,
                MerchantId = Merchant.Id,
                MerchantRef = transactionRequest.TransactionRef,
                TransactionStatusId = (int)TransactionStatusID.Pending,
                SourceType = "PGW API",
                TransactionId = Guid.NewGuid()               
            };

            _unitOfWork.Transactions.Create(Trans);
            _unitOfWork.Save();

            Trans.CardDetail = CardDetails;
            Trans.Currency = currency;

            //Make Bank API Call via Repository
            var ProcessedTransaction = await _unitOfWork.Transactions.ProcessAquiringBankTrasactionAsync(Trans);

            _logger.LogInformation($"Trsancation Ref -{ProcessedTransaction.TransactionId} Bank Ref - {ProcessedTransaction.BankRef} StatusID - {ProcessedTransaction.TransactionStatusId}");

            //Update the Transaction in DB
            _unitOfWork.Transactions.Update(ProcessedTransaction);   
            _unitOfWork.Save();

            //TODO - Save the Transaction history record here for Audit purposes - Useful for delayed transactions

            if (ProcessedTransaction.TransactionStatusId != (int)TransactionStatusID.Completed)
                return BadRequest("Transaction not sucessful");

            //If all ok then return Transaction Response
            var TransactionRes = ProcessedTransaction.ToTransactionResponse();

            //Send Response to Merchant
            return Created(string.Empty, TransactionRes);
        }

        [HttpGet]
        [Route("{TransactionId}")]
        public async Task<IActionResult> GetTransaction([FromRoute] string TransactionId)
        {
            //Validate Transaction request  
            if (!Guid.TryParse(TransactionId.ToString(), out var TransactionGuId))
                return BadRequest("Invalid Transaction Parameters");

            //check if Transaction exists
            var ProcessedTransaction = _unitOfWork.Transactions.GetTrasactionByRef(TransactionGuId.ToString());

            if (ProcessedTransaction == null)
                return NotFound("Transaction not found");

            return Ok(ProcessedTransaction.ToTransactionResponse());
        }
         
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            //Get authenticated Merchant Ref from Identity Claims
            var AuthenticatedMerchantRef = User?.Identities?.First().Claims?.First().Value;

            if (string.IsNullOrEmpty(AuthenticatedMerchantRef) || !Guid.TryParse(AuthenticatedMerchantRef, out var MerchantRefGUID))
                return BadRequest("Invalid Merchant details");

            //check if Transaction exists
            var Transactions = _unitOfWork.Transactions.GetTrasactionsByMerchantRef(MerchantRefGUID.ToString());

            if (Transactions == null)
                return NotFound("Transactions not found");

            return Ok(Transactions.ToTransactionResponse());
        }
    }
}