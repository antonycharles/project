using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IClientAuthorizationHandler
    {
        Task<AuthenticationResponse> AuthenticationAsync(ClientAuthenticationRequest request);
    }
}