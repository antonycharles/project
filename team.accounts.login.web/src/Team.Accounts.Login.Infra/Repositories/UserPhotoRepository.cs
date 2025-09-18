using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Repositories.Interfaces;
using Team.Accounts.Login.Infra.Requests;
using Team.Accounts.Login.Infra.Settings;
using Microsoft.Extensions.Options;

namespace Team.Accounts.Login.Infra.Repositories
{
    public class UserPhotoRepository : BaseRepository, IUserPhotoRepository
    {

        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        public UserPhotoRepository(
            HttpClient httpClient,
            IClientAuthorizationRepository clientAuthorizationRepository,
            IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _httpClient.BaseAddress = new Uri(options.Value.TeamAccountsApiUrl);
            _clientAuthorizationRepository = clientAuthorizationRepository;
        }

        public async Task UpdateAsync(UserPhotoRequest request)
        {
            await AddToken();
            await base.PostAsync("/UserPhoto", request);
        }
        
        private async Task AddToken()
        {
            var token = await _clientAuthorizationRepository.AuthenticateAsync();

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        }
    }
}