using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Management.Infrastructure.Requests;
using Team.Accounts.Management.Infrastructure.Responses;

namespace Team.Accounts.Management.Infrastructure.Repositories.Interfaces
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