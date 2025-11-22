using System.Net.Http.Json;
using Project.Web.DTOs;

namespace Project.Web.Services
{
    public class ProjectService
    {
        private readonly HttpClient _http;

        public ProjectService(HttpClient client)
        {
            _http = client;
        }

        public Task<List<ProjectDto>> GetAll()
            => _http.GetFromJsonAsync<List<ProjectDto>>("/v1/Project");

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