using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Family.Accounts.Login.Infra.Requests;
using Family.Accounts.Login.Infra.Responses;
using Family.Accounts.Login.Infra.Settings;

namespace Family.Accounts.Login.Infra.Repositories
{
    public class ClientAuthorizationRepository : BaseRepository, IClientAuthorizationRepository
    {
        private static AuthenticationResponse? authenticationResponse;

        private readonly AccountsLoginSettings _options;
        public ClientAuthorizationRepository(HttpClient httpClient, IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.FamilyAccountsApiUrl);
        }

        public async Task<AuthenticationResponse> AuthenticateAsync()
        {
            if(authenticationResponse != null && authenticationResponse.ExpiresIn > DateTime.Now)
                return authenticationResponse;

            authenticationResponse = await base.PostAsync<AuthenticationResponse>("/ClientAuthorization", new ClientAuthenticationRequest{
                ClientId = Guid.Parse(_options.ClientId),
                AppSlug = _options.AppFamilyAccountsApiSlug,
                ClientSecret = _options.ClientSecret
            });

            return authenticationResponse;
        }
    }
}