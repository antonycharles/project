using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using Family.Accounts.Core.Handlers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Family.Accounts.Application.Handlers
{
    public class TokenKeyHandler : ITokenKeyHandler
    {
        private readonly IDistributedCache _cache;
        private readonly string KEY_CACHE = "family:accounts:token:key";

        public TokenKeyHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        TokenValidationParameters TokenValidationParams = new TokenValidationParameters
        {
            ValidIssuer = "www.mysite.com",
            ValidAudience = "your-spa",
        };

        public ECDsaSecurityKey GetPrivateKey()
        {
            var jsonWebKey = GetJsonWebKey();

            var key = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                D = Base64UrlEncoder.DecodeBytes(jsonWebKey.D),
                Q = new ECPoint
                {
                    X = Base64UrlEncoder.DecodeBytes(jsonWebKey.X),
                    Y = Base64UrlEncoder.DecodeBytes(jsonWebKey.Y)
                }
            }); 


            return new ECDsaSecurityKey(key)
            {
                KeyId = jsonWebKey.KeyId,
            };

        }


        public IList<JsonWebKey> GetPublicKeys(){

            List<JsonWebKey> keys = new List<JsonWebKey>();

            var key = GetPrivateKey();

            var parameters = key.ECDsa.ExportParameters(true);
            var jwk = new JsonWebKey()
            {
                Kty = JsonWebAlgorithmsKeyTypes.EllipticCurve,
                Use = "sig",
                Kid = key.KeyId,
                KeyId = key.KeyId,
                X = Base64UrlEncoder.Encode(parameters.Q.X),
                Y = Base64UrlEncoder.Encode(parameters.Q.Y),
                //D = Base64UrlEncoder.Encode(parameters.D),
                Crv = JsonWebKeyECTypes.P256,
                Alg = "ES256"
            };

            keys.Add(jwk);

            return keys;
        }

        

        private JsonWebKey GetJsonWebKey()
        {
            var key = _cache.GetString(KEY_CACHE);
            if (key == null)
            {
                var jwk = GenerateJsonWebKey();
                _cache.SetString(KEY_CACHE, JsonSerializer.Serialize(jwk));
                return jwk;
            }

            return JsonSerializer.Deserialize<JsonWebKey>(key);
        }

        private JsonWebKey GenerateJsonWebKey()
        {
            var key = new ECDsaSecurityKey(ECDsa.Create(ECCurve.NamedCurves.nistP256))
            {
                KeyId = Guid.NewGuid().ToString()
            };

            var parameters = key.ECDsa.ExportParameters(true);
            var jwk = new JsonWebKey()
            {
                Kty = JsonWebAlgorithmsKeyTypes.EllipticCurve,
                Use = "sig",
                Kid = key.KeyId,
                KeyId = key.KeyId,
                X = Base64UrlEncoder.Encode(parameters.Q.X),
                Y = Base64UrlEncoder.Encode(parameters.Q.Y),
                D = Base64UrlEncoder.Encode(parameters.D),
                Crv = JsonWebKeyECTypes.P256,
                Alg = "ES256"
            };

            return jwk;
        }
    }
}