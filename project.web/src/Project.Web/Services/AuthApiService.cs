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
            var url = $"/Login/token?code={code}";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<OAuthResponse>();
            return json?.Token ?? string.Empty;
        }

        public class OAuthResponse
        {
            public Guid AuthId { get; set; }
            public DateTime? ExpiresIn { get; set; }
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public string? CallbackUrl { get; set; }
        }
    }
}