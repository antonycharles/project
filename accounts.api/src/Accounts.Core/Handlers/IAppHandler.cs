using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IAppHandler
    {
        Task<PaginatedResponse<AppResponse>> GetAppsAsync(PaginatedRequest request);
        Task<AppResponse> GetByIdAsync(Guid id);
        Task<AppResponse> CreateAsync(AppRequest request);
        Task UpdateAsync(Guid id, AppRequest request);
        Task DeleteAsync(Guid id);
    }
}