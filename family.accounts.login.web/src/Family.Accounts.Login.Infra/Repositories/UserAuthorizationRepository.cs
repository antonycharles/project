using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Exceptions;
using Family.Accounts.Login.Infra.Repositories.Interfaces;
using Family.Accounts.Login.Infra.Requests;
using Family.Accounts.Login.Infra.Responses;
using Family.Accounts.Login.Infra.Settings;
using Microsoft.Extensions.Options;

namespace Family.Accounts.Login.Infra.Repositories
{
    public class UserAuthorizationRepository : BaseRepository, IUserAuthorizationRepository
    {
        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        public UserAuthorizationRepository(
            HttpClient httpClient, 
            IClientAuthorizationRepository clientAuthorizationRepository,
            IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(options.Value.FamilyAccountsApiUrl);
            _clientAuthorizationRepository = clientAuthorizationRepository;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(UserAuthenticationRequest request)
        {
            await AddToken();
            var code = await base.PostAsync<AuthenticationCodeResponse>("OAuth/Authentication", request);

            if (code == null || string.IsNullOrWhiteSpace(code.Code))
                throw new ExternalApiException("Error to get authentication code.");

            return await base.PostFormDataAsync<AuthenticationResponse>("OAuth/token", new Dictionary<string, string>
            {
                { "code", code.Code },
                { "GrantType", "authorization_code" }
            });
        }

        protected async Task AddToken()
        {
            var token = await _clientAuthorizationRepository.AuthenticateAsync();

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        }

        public async Task<UserResponse> GetUserInfoByTokenAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.GetAsync<UserResponse>($"OAuth/userInfo");
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync(string tokenRefresh, string appSlug = "")
        {
            await AddToken();
            return await base.PostFormDataAsync<AuthenticationResponse>("OAuth/token", new Dictionary<string, string>
            {
                { "RefreshToken", tokenRefresh },
                { "GrantType", "refresh_token" },
                { "AppSlug", appSlug }
            });
        }
    }
}