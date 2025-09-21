using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Accounts.Login.Infra.Repositories.Interfaces;
using Accounts.Login.Infra.Requests;
using Accounts.Login.Infra.Responses;
using Accounts.Login.Infra.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Accounts.Login.Infra.Repositories
{
    public class FileRepository : BaseRepository, IFileRepository
    {

        private readonly IClientAuthorizationRepository _clientAuthorizationRepository;
        private readonly AccountsLoginSettings _options;

        public FileRepository(
            HttpClient httpClient,
            IClientAuthorizationRepository clientAuthorizationRepository,
            IOptions<AccountsLoginSettings> options) : base(httpClient)
        {
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.FileApiUrl);
            _clientAuthorizationRepository = clientAuthorizationRepository;
        }

        public async Task<FileDocumentResponse> UploadAsync(IFormFile file)
        {
            await AddToken();
            return await base.PostFileAsync<FileDocumentResponse>("Upload", file);
        }
        
        private async Task AddToken()
        {
            var token = await _clientAuthorizationRepository.AuthenticateAsync(_options.FileApiSlug);

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        }
    }
}