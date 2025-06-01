using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Requests;
using Family.Accounts.Login.Infra.Responses;
using Family.Accounts.Login.Infra.Settings;
using Microsoft.Extensions.Options;

namespace Family.Accounts.Login.Infra.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        public UserRepository(
            HttpClient httpClient, 
            IClientAuthorizationRepository clientAuthorizationRepository,
            IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(options.Value.FamilyAccountsApiUrl);
            _clientAuthorizationRepository = clientAuthorizationRepository;
        }

        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            await AddToken();
            return await base.PostAsync<UserResponse>("/User", request);
        }

        private async Task AddToken()
        {
            var token = await _clientAuthorizationRepository.AuthenticateAsync();

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        }
        
        public async Task UpdateAsync(Guid userId, UserRequest request)
        {
            await AddToken();
            await base.PutAsync("/User", request);
        }
    }
}