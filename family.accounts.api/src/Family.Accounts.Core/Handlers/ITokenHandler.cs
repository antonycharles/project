using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface ITokenHandler
    {
        AuthenticationResponse GenerateToken(Client client);
        AuthenticationResponse GenerateToken(User user, UserAuthenticationRequest request);
        bool ValidateToken(string token);
    }
}