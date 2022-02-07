using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MyApp.Constants;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MyApp.WebApi
{
    public class MixedAuthenticationHandler : CookieAuthenticationHandler
    {
        public MixedAuthenticationHandler(IOptionsMonitor<CookieAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authResult = await base.HandleAuthenticateAsync();
            if (!authResult.Succeeded)
            {
                string authorizationHeader = Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(authorizationHeader))

                {
                    return AuthenticateResult.Fail("Auth header not found.");
                }

                var ticket = authorizationHeader[(authorizationHeader.IndexOf(' ') + 1)..];

                var kerberosAuthTicketValidator = new KerberosAuthTicketValidator();
                var identity = await kerberosAuthTicketValidator.IsValid(ticket, "web.keytab");


                if (identity != null)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var authTicket = new AuthenticationTicket(principal, AuthentificationContants.AuthenticationScheme);
                    if (ticket != null)
                    {
                        await base.HandleSignInAsync(principal, authTicket.Properties);
                        return AuthenticateResult.Success(authTicket);
                    }
                }
            }

            return authResult;
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            Response.Headers[HeaderNames.WWWAuthenticate] = AuthentificationContants.AuthorizationHeader;
            return Task.CompletedTask;
        }
    }
}
