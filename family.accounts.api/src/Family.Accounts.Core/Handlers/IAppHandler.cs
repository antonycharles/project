using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface IAppHandler
    {
        Task<PaginatedResponse<App>> GetAppsAsync(PaginatedRequest request);
        Task<App> GetByIdAsync(Guid id);
        Task<App> CreateAsync(AppRequest request);
        Task UpdateAsync(Guid id, AppRequest request);
        Task DeleteAsync(Guid id);
    }
}