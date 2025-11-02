using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(UserRequest request);
        Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest? request);
        Task<UserResponse> GetByIdAsync(Guid id, Guid companyId);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, UserRequest request);
    }
}