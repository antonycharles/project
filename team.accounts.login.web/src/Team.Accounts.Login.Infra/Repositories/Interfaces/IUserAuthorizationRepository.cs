using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Requests;
using Team.Accounts.Login.Infra.Responses;

namespace Team.Accounts.Login.Infra.Repositories.Interfaces
{
    public interface IUserAuthorizationRepository
    {
        Task<AuthenticationResponse> AuthenticateAsync(UserAuthenticationRequest request);
        Task<AuthenticationResponse> RefreshTokenAsync(string tokenRefresh, string appSlug = "");
        Task<UserResponse> GetUserInfoByTokenAsync(string token);
    }
}