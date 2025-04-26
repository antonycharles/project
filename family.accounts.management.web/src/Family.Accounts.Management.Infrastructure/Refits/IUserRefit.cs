using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;
using Refit;

namespace Family.Accounts.Management.Infrastructure.Refits
{
    public interface IUserRefit
    {
        [Post("/User")]
        Task<UserResponse> CreateAsync(UserRequest request);

        [Get("/User")]
        Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest? request);
        
        [Delete("/User/{id}")]
        Task DeleteAsync(Guid id);

        [Get("/User/{id}")]
        Task<UserResponse> GetByIdAsync(Guid id);

        [Put("/User/{id}")]
        Task UpdateAsync(Guid id, UserRequest request);
    }
}