using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
{
    public interface ITokenHandler
    {
        Task<AuthenticationResponse> GenerateClientTokenAsync(Guid clientId, string appSlug);
        Task<AuthenticationResponse> GenerateUserTokenAsync(Guid userId, string? appSlug);
        bool ValidateToken(string token);
    }
}