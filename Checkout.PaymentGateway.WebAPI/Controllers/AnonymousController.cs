using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.PaymentGateway.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("v1")]
    public class AnonymousController : Controller
    {
        [HttpGet]
        [Route("HealthCheck")]
        public IActionResult Index()
        {
            return Ok("API is healthy");
        }
    }
}