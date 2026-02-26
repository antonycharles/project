using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IAppCallbackRepository
    {
        Task<IEnumerable<AppCallbackResponse>> GetAllByAppIdAsync(Guid appId);
        Task<AppCallbackResponse> GetByIdAsync(Guid id);
        Task CreateAsync(AppCallbackRequest request);
        Task UpdateAsync(Guid id, AppCallbackRequest request);
        Task DeleteAsync(Guid id);
    }
}
