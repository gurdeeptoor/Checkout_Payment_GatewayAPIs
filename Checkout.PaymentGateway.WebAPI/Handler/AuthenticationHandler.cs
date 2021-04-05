using Checkout.PaymentGateway.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.WebAPI
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IMerchantRepository _merchantRepository;
        public AuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                          ILoggerFactory logger,
                                          UrlEncoder encoder,
                                          ISystemClock clock,
                                          IMerchantRepository merchantRepository) : base(options, logger, encoder, clock)
        {
            _merchantRepository = merchantRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("X-API-KEY"))
                return AuthenticateResult.Fail("Invalid header");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["X-API-KEY"]);           
            var merChant = _merchantRepository.GetMerchantByKey(authHeader.Scheme);

            if (merChant != null)
            {
                //Save Merchant ref as claim so it can be used in the application later if needed
                var claims = new[] { new Claim(ClaimTypes.Name, merChant.MerchantRef.ToString()) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return await Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return await Task.FromResult(AuthenticateResult.Fail("Invalid Auth header"));
        }
    }
}