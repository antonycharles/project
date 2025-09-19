using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface ITokenHandler
    {
        Task<AuthenticationResponse> GenerateClientTokenAsync(Guid clientId, string appSlug);
        Task<AuthenticationResponse> GenerateUserTokenAsync(Guid userId, string? appSlug);
        bool ValidateToken(string token);
    }
}