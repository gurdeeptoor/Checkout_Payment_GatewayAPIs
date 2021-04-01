using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.MockBank.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MockBankController : ControllerBase
    {
        //Add List of Credit Cards with Holder Name, Address, Balances

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MockBankController> _logger;

        public MockBankController(ILogger<MockBankController> logger)
        {
            _logger = logger;
        }        
    }
}
