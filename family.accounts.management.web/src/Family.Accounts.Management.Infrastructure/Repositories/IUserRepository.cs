using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;

namespace Family.Accounts.Management.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(UserRequest request);
        Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest? request);
        Task<UserResponse> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, UserRequest request);
    }
}