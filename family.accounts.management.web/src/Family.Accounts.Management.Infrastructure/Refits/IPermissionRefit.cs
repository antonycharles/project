using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Refits
{
    public interface IPermissionRefit
    {
        
        [Get("/Permission")]
        Task<ApiResponse<PaginatedResponse<PermissionResponse>>> GetAsync(PermissionPaginatedRequest? request);
    }
}