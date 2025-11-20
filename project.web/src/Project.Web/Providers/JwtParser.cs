using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Web.Providers
{
    public static class JwtParser
    {
        public static ClaimsIdentity ParseClaimsFromJwt(string token)
        {
            var payload = token.Split('.')[1];
            var json = ParseBase64WithoutPadding(payload);
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            var claims = new List<Claim>();

            foreach (var item in data)
            {
                claims.Add(new Claim(item.Key, item.Value.ToString()!));
            }

            return new ClaimsIdentity(claims, "jwt");
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}