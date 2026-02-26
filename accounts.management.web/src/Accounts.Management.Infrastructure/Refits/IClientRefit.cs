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
        [Post("/Client")]
        Task<ClientResponse> CreateAsync(ClientRequest request);

        [Get("/Client")]
        Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest? request);
        
        [Get("/Client/{id}")]
        Task<ClientResponse> GetByIdAsync(Guid id);

        [Put("/Client/{id}")]
        Task UpdateAsync(Guid id, ClientUpdateRequest request);

        [Delete("/Client/{id}")]
        Task DeleteAsync(Guid id);
    }
}
