using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IPermissionHandler
    {
        Task<PaginatedResponse<PermissionResponse>> GetAsync(PaginatedPermissionRequest request);
        Task<PermissionResponse> GetByIdAsync(Guid id);
        Task<PermissionResponse> CreateAsync(PermissionRequest request);
        Task UpdateAsync(Guid id, PermissionRequest request);
        Task DeleteAsync(Guid id);
    }
}