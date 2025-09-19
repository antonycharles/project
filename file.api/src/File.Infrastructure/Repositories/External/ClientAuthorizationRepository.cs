using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Infrastructure.DTOs;
using File.Infrastructure.interfaces.External;
using File.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace File.Infrastructure.Repositories.External
{
    public class ClientAuthorizationRepository : BaseApiRepository, IClientAuthorizationRepository
    {
        private static AuthenticationDTO? authenticationResponse;
        private readonly FileSettings _options;
        public ClientAuthorizationRepository(HttpClient httpClient, IOptions<FileSettings> options) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.AccountsApiUrl);
        }

        public async Task<AuthenticationDTO> AuthenticateAsync()
        {
            if(authenticationResponse != null && authenticationResponse.ExpiresIn > DateTime.Now)
                return authenticationResponse;

            authenticationResponse = await base.PostAsync<AuthenticationDTO>("/ClientAuthorization", new ClientAuthenticationDTO{
                ClientId = Guid.Parse(_options.ClientId),
                AppSlug = _options.AppAccountsApiSlug,
                ClientSecret = _options.ClientSecret
            });

            return authenticationResponse;
        }
    }
}