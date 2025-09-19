using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Accounts.Management.Infrastructure.Refits
{
    public interface IClientRefit
    {
        [Get("/Client")]
        Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest? request);
        
        [Get("/Client/{id}")]
        Task<ClientResponse> GetByIdAsync(Guid id);
    }
}