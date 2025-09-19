using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Accounts.Login.Infra.Requests;
using Accounts.Login.Infra.Responses;
using Accounts.Login.Infra.Settings;
using Accounts.Login.Infra.Repositories.Interfaces;

namespace Accounts.Login.Infra.Repositories
{
    public class ClientAuthorizationRepository : BaseRepository, IClientAuthorizationRepository
    {
        private static AuthenticationResponse? authenticationResponse;

        private readonly AccountsLoginSettings _options;
        public ClientAuthorizationRepository(HttpClient httpClient, IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.AccountsApiUrl);
        }

        public async Task<AuthenticationResponse> AuthenticateAsync()
        {
            if(authenticationResponse != null && authenticationResponse.ExpiresIn > DateTime.Now)
                return authenticationResponse;

            authenticationResponse = await base.PostFormDataAsync<AuthenticationResponse>("OAuth/token", new Dictionary<string, string>
            {
                { "ClientId", _options.ClientId },
                { "AppSlug", _options.AppAccountsApiSlug },
                { "ClientSecret", _options.ClientSecret },
                { "GrantType", "client_credentials" }
            });

            return authenticationResponse;
        }
    }
}