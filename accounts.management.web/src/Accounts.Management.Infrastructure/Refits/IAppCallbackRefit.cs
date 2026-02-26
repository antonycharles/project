using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Refits
{
    public interface IAppCallbackRefit
    {
        [Get("/AppCallback/app/{appId}")]
        Task<IEnumerable<AppCallbackResponse>> GetAllByAppIdAsync(Guid appId);

        [Get("/AppCallback/{id}")]
        Task<AppCallbackResponse> GetByIdAsync(Guid id);

        [Post("/AppCallback")]
        Task<AppCallbackResponse> CreateAsync(AppCallbackRequest request);

        [Put("/AppCallback/{id}")]
        Task UpdateAsync(Guid id, AppCallbackRequest request);

        [Delete("/AppCallback/{id}")]
        Task DeleteAsync(Guid id);
    }
}
