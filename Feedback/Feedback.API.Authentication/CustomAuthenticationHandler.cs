using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Feedback.API.Authentication
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Swagger doesn't work well with non alpha numerica values in the header so as a workaround I also accept UbiUserId
            if (!(Request.Headers.ContainsKey("ubi‐userid") || Request.Headers.ContainsKey("ubiuserid")))
                return AuthenticateResult.Fail("Unauthorized");

            string userId = !string.IsNullOrEmpty(Request.Headers["ubi‐userid"]) ? Request.Headers["ubi‐userid"] : Request.Headers["ubiuserid"];
            if (string.IsNullOrEmpty(userId))
            {
                return AuthenticateResult.NoResult();
            }

            return Authenticate(userId);
        }

        private AuthenticateResult Authenticate(string token)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, token),
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
