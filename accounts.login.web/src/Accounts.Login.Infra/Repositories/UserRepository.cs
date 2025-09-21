using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Requests;
using Accounts.Login.Infra.Responses;
using Accounts.Login.Infra.Settings;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Infra.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        private readonly AccountsLoginSettings _options;

        public UserRepository(
            HttpClient httpClient,
            IClientAuthorizationRepository clientAuthorizationRepository,
            IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.AccountsApiUrl);
            _clientAuthorizationRepository = clientAuthorizationRepository;
        }

        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            await AddToken();
            return await base.PostAsync<UserResponse>("User", request);
        }

        private async Task AddToken()
        {
            var token = await _clientAuthorizationRepository.AuthenticateAsync(_options.AppAccountsApiSlug);

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        }
        
        public async Task UpdateAsync(Guid userId, UserUpdateRequest request)
        {
            await AddToken();
            await base.PutAsync($"User/{userId}", request);
        }
    }
}