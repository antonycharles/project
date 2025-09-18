using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Team.File.Infrastructure.interfaces.External;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Team.File.Api.Authorization
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenHandler(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var keys = await _tokenRepository.GetPublicKeysAsync();


            if (keys == null || !keys.Any())
                return false;


            var jsonWebKey = keys.First();

            var ecd = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                Q = new ECPoint
                {
                    X = Base64UrlEncoder.DecodeBytes(jsonWebKey.X),
                    Y = Base64UrlEncoder.DecodeBytes(jsonWebKey.Y)
                }
            }); 

            var key = new ECDsaSecurityKey(ecd)
            {
                KeyId = jsonWebKey.Kid,
            };

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}