using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Accounts.Login.Infra.Exceptions;
using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Responses;
using Accounts.Login.Infra.Settings;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Infra.Repositories
{
    public class AppRepository : BaseRepository, IAppRepository
    {
        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        private readonly AccountsLoginSettings _options;
        public AppRepository(
            HttpClient httpClient, 
            IClientAuthorizationRepository clientAuthorizationRepository,
            IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.AccountsApiUrl);
            _clientAuthorizationRepository = clientAuthorizationRepository;
        }

        public async Task<PaginatedResponse<AppResponse>> GetPublicAppsByUserIdAsync(Guid userId)
        {
            try
            {
                await AddToken();
                return await base.GetAsync<PaginatedResponse<AppResponse>>($"App?userId={userId}&isPublic=true");
            }
            catch (ExternalApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ExternalApiException($"Error in request: {ex.Message}", ex);
            }
        }


        protected async Task AddToken()
        {
            var token = await _clientAuthorizationRepository.AuthenticateAsync(_options.AppAccountsApiSlug);

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        }

    }
}
