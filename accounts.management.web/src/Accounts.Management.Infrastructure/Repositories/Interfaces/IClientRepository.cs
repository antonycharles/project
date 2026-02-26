using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task CreateAsync(ClientRequest request);
        Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest? request);
        Task<ClientResponse> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, ClientUpdateRequest request);
        Task DeleteAsync(Guid id);
    }
}
