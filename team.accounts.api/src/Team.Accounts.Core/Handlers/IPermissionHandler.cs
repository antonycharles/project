using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
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