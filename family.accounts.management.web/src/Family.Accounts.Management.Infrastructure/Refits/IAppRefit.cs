using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Refits
{
    public interface IAppRefit
    {
        [Post("/App")]
        Task<AppResponse> CreateAsync(AppRequest request);

        [Get("/App")]
        Task<ApiResponse<PaginatedResponse<AppResponse>>> GetAsync(PaginatedRequest? request);
        
        [Delete("/App/{id}")]
        Task DeleteAsync(Guid id);

        [Get("/App/{id}")]
        Task<AppResponse> GetByIdAsync(Guid id);

        [Put("/App/{id}")]
        Task UpdateAsync(Guid id, AppRequest request);
    }
}