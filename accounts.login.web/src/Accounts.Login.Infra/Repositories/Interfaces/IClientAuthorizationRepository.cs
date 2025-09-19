using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Requests;
using Accounts.Login.Infra.Responses;

namespace Accounts.Login.Infra.Repositories.Interfaces
{
    public interface IClientAuthorizationRepository
    {
        Task<AuthenticationResponse> AuthenticateAsync();
    }
}