using Checkout.PaymentGateway.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Core
{    public interface ICardRepository : IRepository<CardDetail>
    {
        bool IsCardValid(string CardNumber, int ExpMonth, int ExpYear);
    }
}
