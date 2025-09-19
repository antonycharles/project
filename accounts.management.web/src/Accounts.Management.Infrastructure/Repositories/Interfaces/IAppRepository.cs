using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IAppRepository
    {
        Task CreateAsync(AppRequest request);
        Task<PaginatedResponse<AppResponse>> GetAsync(PaginatedRequest? request);
        Task<AppResponse> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, AppRequest request);
    }
}