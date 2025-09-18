using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Exceptions;
using Family.Accounts.Management.Infrastructure.Refits;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Family.Accounts.Management.Web.Handlers
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IClientAuthorizationRefit _httpContextAccessor;
        private readonly AccountsManagementSettings  _settings;
        private static DateTime? _tokenExpirationTime;
        private static string _token;

        public AuthHeaderHandler(IClientAuthorizationRefit httpContextAccessor, IOptions<AccountsManagementSettings> settings)
        {
            _settings = settings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetBearerTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetBearerTokenAsync()
        {
            try
            {
                if (_tokenExpirationTime.HasValue && _tokenExpirationTime > DateTime.Now)
                    return _token;
                
                // Request a new token
                var response = await _httpContextAccessor.AuthorizationAsync(
                    new Dictionary<string, object>
                    {
                        { "GrantType", "client_credentials" },
                        { "ClientId", new Guid(_settings.ClientId)},
                        { "ClientSecret", _settings.ClientSecret },
                        { "AppSlug", _settings.FamilyAccountsApiSlug }
                    });

                if (response != null && response.Token != null)
                {
                    _tokenExpirationTime = response?.ExpiresIn;
                    _token = response?.Token ?? string.Empty;
                }

                return response?.Token ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new ExternalApiException("Error retrieving bearer token", ex);
            }
        }
    }
}