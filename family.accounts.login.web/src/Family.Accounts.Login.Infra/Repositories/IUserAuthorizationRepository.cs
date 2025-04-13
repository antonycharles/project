using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Requests;
using Family.Accounts.Login.Infra.Responses;

namespace Family.Accounts.Login.Infra.Repositories
{
    public interface IUserAuthorizationRepository
    {
        Task<AuthenticationResponse> AuthenticateAsync(UserAuthenticationRequest request);
    }
}