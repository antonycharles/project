using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
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