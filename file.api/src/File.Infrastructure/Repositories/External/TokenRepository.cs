using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using File.Infrastructure.DTOs;
using File.Infrastructure.interfaces.External;
using File.Infrastructure.Settings;
using System.Net.Http.Headers;

namespace File.Infrastructure.Repositories.External
{
    public class TokenRepository : BaseApiRepository, ITokenRepository
    {

        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        private readonly FileSettings _options;
        public TokenRepository(HttpClient httpClient, IOptions<FileSettings> options, IClientAuthorizationRepository clientAuthorizationRepository) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.AccountsApiUrl);
            _clientAuthorizationRepository = clientAuthorizationRepository;
        }

        public async Task<List<JsonWebKeyDTO>> GetPublicKeysAsync()
        {
            await AddToken();
            return await base.GetAsync<List<JsonWebKeyDTO>>("/Token/public-key");
        }



        private async Task AddToken()
        {
            var token = await _clientAuthorizationRepository.AuthenticateAsync();

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        }
    }
}