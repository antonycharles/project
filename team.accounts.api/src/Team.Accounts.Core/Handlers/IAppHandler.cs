using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
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