using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IAppCallbackHandler
    {
        Task<IEnumerable<AppCallbackResponse>> GetAllByAppIdAsync(Guid appId);
        Task<AppCallbackResponse> GetByIdAsync(Guid id);
        Task<AppCallbackResponse> CreateAsync(AppCallbackRequest entity);
        Task UpdateAsync(Guid id, AppCallbackRequest entity);
        Task DeleteAsync(Guid id);
    }
}