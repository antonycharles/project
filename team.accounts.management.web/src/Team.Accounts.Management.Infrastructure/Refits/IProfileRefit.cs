using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Requests;
using Team.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Team.Accounts.Management.Infrastructure.Refits
{
    public interface IProfileRefit
    {
        [Post("/Profile")]
        Task<ProfileResponse> CreateAsync(ProfileRequest request);

        [Get("/Profile")]
        Task<ApiResponse<PaginatedResponse<ProfileResponse>>> GetAsync(ProfilePaginatedRequest? request);
        
        [Delete("/Profile/{id}")]
        Task DeleteAsync(Guid id);

        [Get("/Profile/{id}")]
        Task<ProfileResponse> GetByIdAsync(Guid id);

        [Put("/Profile/{id}")]
        Task UpdateAsync(Guid id, ProfileRequest request);
    }
}