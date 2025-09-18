using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Team.Accounts.Core.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Team.Accounts.Api.Helpers
{
    // https://sardarmudassaralikhan.medium.com/custom-jwt-handler-in-asp-net-core-7-web-api-c3af9e423b52
    // https://jasonwatmore.com/post/2022/01/19/net-6-create-and-validate-jwt-tokens-use-custom-jwt-middleware
    public class CustomJwtBearerHandler : JwtBearerHandler
    {
        private readonly ITokenHandler _tokenHandler;
        public CustomJwtBearerHandler(
            ITokenHandler tokenHandler, 
            IOptionsMonitor<JwtBearerOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, ISystemClock clock) 
        : base(options, logger, encoder, clock)
        {
            _tokenHandler = tokenHandler;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues))
            {
                return AuthenticateResult.Fail("Authorization header not found.");
            }

            var authorizationHeader = authorizationHeaderValues.FirstOrDefault();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return AuthenticateResult.Fail("Bearer token not found in Authorization header.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var response = _tokenHandler.ValidateToken(token);

            if (response == false)
            {
                return AuthenticateResult.Fail("Token validation failed.");
            }       

            var principal = GetClaims(token);

            return AuthenticateResult.Success(new AuthenticationTicket(principal, "CustomJwtBearer"));
        }


        private ClaimsPrincipal GetClaims(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(Token) as JwtSecurityToken;

            var claimsIdentity = new ClaimsIdentity(token.Claims, "Token");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }
    }
}