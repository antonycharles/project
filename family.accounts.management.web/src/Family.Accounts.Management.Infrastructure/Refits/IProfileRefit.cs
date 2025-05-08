using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Refits
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