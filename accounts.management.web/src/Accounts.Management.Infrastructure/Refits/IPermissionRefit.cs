using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Accounts.Management.Infrastructure.Refits
{
    public interface IPermissionRefit
    {
        [Post("/Permission")]
        Task<PermissionResponse> CreateAsync(PermissionRequest request);

        [Get("/Permission")]
        Task<ApiResponse<PaginatedResponse<PermissionResponse>>> GetAsync(PermissionPaginatedRequest? request);

        [Get("/Permission/{id}")]
        Task<PermissionResponse> GetByIdAsync(Guid id);

        [Put("/Permission/{id}")]
        Task UpdateAsync(Guid id, PermissionRequest request);

        [Delete("/Permission/{id}")]
        Task DeleteAsync(Guid id);
    }
}
