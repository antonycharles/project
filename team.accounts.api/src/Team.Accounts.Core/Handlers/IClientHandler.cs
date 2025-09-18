using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
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