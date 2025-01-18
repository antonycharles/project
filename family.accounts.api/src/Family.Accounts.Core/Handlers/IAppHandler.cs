using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
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