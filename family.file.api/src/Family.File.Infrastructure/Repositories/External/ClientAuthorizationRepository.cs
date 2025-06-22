using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Infrastructure.DTOs;
using Family.File.Infrastructure.interfaces.External;
using Family.File.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Family.File.Infrastructure.Repositories.External
{
    public class ClientAuthorizationRepository : BaseApiRepository, IClientAuthorizationRepository
    {
        private static AuthenticationDTO? authenticationResponse;
        private readonly FileSettings _options;
        public ClientAuthorizationRepository(HttpClient httpClient, IOptions<FileSettings> options) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.FamilyAccountsApiUrl);
        }

        public async Task<AuthenticationDTO> AuthenticateAsync()
        {
            if(authenticationResponse != null && authenticationResponse.ExpiresIn > DateTime.Now)
                return authenticationResponse;

            authenticationResponse = await base.PostAsync<AuthenticationDTO>("/ClientAuthorization", new ClientAuthenticationDTO{
                ClientId = Guid.Parse(_options.ClientId),
                AppSlug = _options.AppFamilyAccountsApiSlug,
                ClientSecret = _options.ClientSecret
            });

            return authenticationResponse;
        }
    }
}