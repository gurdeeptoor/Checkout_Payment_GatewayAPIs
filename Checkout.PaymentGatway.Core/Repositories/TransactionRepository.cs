﻿using Checkout.PaymentGateway.Data;
using CheckOut.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TransactionStatusID = CheckOut.Common.TransactionStatusID;

namespace Checkout.PaymentGateway.Core
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private CheckOutDBContext _checkOutDBContext;
        private BankAPISettings _bankAPISettings;
        public TransactionRepository(CheckOutDBContext checkOutDBContext, BankAPISettings bankAPISettings) : base(checkOutDBContext)
        {
            _checkOutDBContext = checkOutDBContext;
            _bankAPISettings = bankAPISettings;
        }

        public Transaction GetTrasactionByRef(string RefNo)
        {
            if (Guid.TryParse(RefNo, out var uRefNo))
            {
                var Txn = _checkOutDBContext.Transactions.FirstOrDefault(x => x.TransactionId == uRefNo);

                if (Txn != null)
                {
                    Txn.CardDetail = _checkOutDBContext.CardDetails.FirstOrDefault(x => x.Id == Txn.CardDetailId);
                    Txn.Currency = _checkOutDBContext.Currencies.FirstOrDefault(x => x.Id == Txn.CurrencyId);

                    return Txn;
                }
            }
            return null;
        }

        public async Task<Transaction> ProcessAquiringBankTrasactionAsync(Transaction Transaction)
        {
            var requestData = new BankTransactionRequest
            {
                Amount = Transaction.Amount,
                CardNum = Transaction.CardDetail.CardNum,
                Cvv = Transaction.CardDetail.Cvv,
                ExpMonth = Transaction.CardDetail.ExpMonth,
                ExpYear = Transaction.CardDetail.ExpYear,
                HolderName = Transaction.CardDetail.HolderName,
                MerchantRef = Transaction.MerchantRef,
                PaymentGatewayRef = "PGW01",
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_bankAPISettings.APIUri);
                //HTTP GET
                var json = JsonConvert.SerializeObject(requestData);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("/v1/BankTransactions/payment", data);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var resp = await result.Content.ReadAsStringAsync();

                    var bankTransactionResponse = JsonConvert.DeserializeObject<BankTransactionResponse>(resp);
                    //Process here 

                    Transaction.AuthCode = bankTransactionResponse.AuthCode;
                    Transaction.BankRef = bankTransactionResponse.BankRef;
                    Transaction.TransactionStatusId = (int)TransactionStatusID.Completed;
                    return Transaction;
                }

                //Additional status handling ommitted for brevity 
                Transaction.TransactionStatusId = (int)TransactionStatusID.Rejected;
                return Transaction;
            }
        }
    }
}