using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Team.File.Api.Extensions;

namespace Team.File.Api.Helpers
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            var idClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Id);
            return idClaim?.Value != null ? new Guid(idClaim.Value) : Guid.Empty;
        } 
    }
}