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
        Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest? request);
        Task<ClientResponse> GetByIdAsync(Guid id);
    }
}