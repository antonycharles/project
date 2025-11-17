using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Web.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ExchangeCodeForJwtAsync(string code)
        {
            var response = await _httpClient.PostAsJsonAsync("/oauth/token", new
            {
                code,
                grant_type = "authorization_code"
            });

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<OAuthResponse>();
            return json?.AccessToken ?? string.Empty;
        }

        public class OAuthResponse
        {
            public string AccessToken { get; set; }
        }
    }
}