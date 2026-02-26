using Accounts.Management.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IClientProfileRepository
    {
        Task CreateAsync(ClientProfileRequest request);
        Task DeleteAsync(Guid id);
    }
}
