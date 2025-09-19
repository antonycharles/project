using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using Accounts.Core.Handlers;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Core.Handlers
{
    public interface ITokenKeyHandler
    {
        ECDsaSecurityKey GetPrivateKey();
        IList<JsonWebKey> GetPublicKeys();
    }
}