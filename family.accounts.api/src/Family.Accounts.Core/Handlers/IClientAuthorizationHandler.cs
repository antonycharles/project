using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface IClientAuthorizationHandler
    {
        Task<AuthenticationResponse> AuthenticationAsync(ClientAuthenticationRequest request);
    }
}