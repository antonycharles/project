using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Web.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetId(this ClaimsPrincipal user)
            => Guid.TryParse(user.FindFirst(CustomClaimTypes.Id)?.Value, out var id)
                ? id
                : Guid.Empty;

        public static Guid CompanyId(this ClaimsPrincipal user)
            => Guid.TryParse(user.FindFirst(CustomClaimTypes.CompanyId)?.Value, out var id)
                ? id
                : Guid.Empty;

        public static string CompanyName(this ClaimsPrincipal user)
            => user.FindFirst(CustomClaimTypes.CompanyName)?.Value ?? "Unknown Company";

        public static string GetName(this ClaimsPrincipal user)
            => user?.FindFirst(CustomClaimTypes.Name)?.Value ?? "Unknown User";

        public static string GetEmail(this ClaimsPrincipal user)
            => user?.FindFirst(CustomClaimTypes.Email)?.Value ?? "Unknown Email";

        public static string GetImage(this ClaimsPrincipal user)
        {
            try
            {
                var image = user.FindFirst(CustomClaimTypes.Image)?.Value;

                if (!string.IsNullOrWhiteSpace(image) &&
                    image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    return image;
                }

                var name = user.GetName();
                return !string.IsNullOrWhiteSpace(name) ? name[..1].ToUpper() : "?";
            }catch
            {
                return "?";   
            }
        }
    }
}