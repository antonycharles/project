using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface IClientHandler
    {
        Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest request);
        Task<ClientResponse> GetByIdAsync(Guid id);
        Task<ClientResponse> CreateAsync(ClientRequest request);
        Task UpdateAsync(Guid id, ClientUpdateRequest request);
        Task DeleteAsync(Guid id);
    }
}