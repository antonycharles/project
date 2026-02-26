using Accounts.Management.Infrastructure.Requests;
using Refit;
using System;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Refits
{
    public interface IClientProfileRefit
    {
        [Post("/ClientProfile")]
        Task CreateAsync(ClientProfileRequest request);

        [Delete("/ClientProfile/{id}")]
        Task DeleteAsync(Guid id);
    }
}
