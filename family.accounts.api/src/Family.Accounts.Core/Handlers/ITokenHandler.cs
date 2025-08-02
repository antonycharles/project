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
        Task<AuthenticationResponse> GenerateClientTokenAsync(Guid clientId, string appSlug);
        Task<AuthenticationResponse> GenerateUserTokenAsync(Guid userId, string? appSlug);
        bool ValidateToken(string token);
    }
}