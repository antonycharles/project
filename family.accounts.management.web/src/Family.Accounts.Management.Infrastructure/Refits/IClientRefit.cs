using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Refits
{
    public interface IClientRefit
    {
        [Get("/Client")]
        Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest? request);
        
        [Get("/Client/{id}")]
        Task<ClientResponse> GetByIdAsync(Guid id);
    }
}