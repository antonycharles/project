using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Project.Web.DTOs;

namespace Project.Web.Services
{
    public class ProjectService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public ProjectService(IConfiguration configuration, AuthenticationStateProvider authStateProvider)
        {
            _http = new HttpClient { BaseAddress = new Uri(configuration["AccountsApiUrl"]) };
            _authStateProvider = authStateProvider;
        }

        private async Task AddBearerTokenAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                // Supondo que o token está salvo como claim "access_token"
                var token = user.FindFirst("access_token")?.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
        }

        public async Task<List<ProjectDto>> GetAll()
        {
            await AddBearerTokenAsync();
            return await _http.GetFromJsonAsync<List<ProjectDto>>("/v1/Project");
        }

        public Task<ProjectDto> Get(Guid id)
            => _http.GetFromJsonAsync<ProjectDto>($"/v1/Project/{id}");

        public async Task<ProjectDto> Create(ProjectCreateDto dto)
        {
            var res = await _http.PostAsJsonAsync("/v1/Project", dto);
            return await res.Content.ReadFromJsonAsync<ProjectDto>();
        }

        public Task Delete(Guid id)
            => _http.DeleteAsync($"/v1/Project/{id}");
    }
}