using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
{
    public interface IUserAuthorizationHandler
    {
        Task<AuthenticationResponse> AuthenticationAsync(UserAuthenticationRequest request);
    }
}