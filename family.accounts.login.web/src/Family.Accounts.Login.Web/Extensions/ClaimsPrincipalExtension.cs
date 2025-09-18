using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Family.Accounts.Login.Web.Extensions
{
    public static class ClaimsPrincipalExtension
    {

        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            var idClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
            return idClaim?.Value != null ? new Guid(idClaim.Value) : Guid.Empty;
        }

        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            var nameClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            return nameClaim?.Value ?? "Unknown User";
        }

        public static string? GetRefreshToken(this ClaimsPrincipal claimsPrincipal)
        {
            var refreshTokenClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "RefreshToken");
            return refreshTokenClaim?.Value ?? null;
        }
    }
}