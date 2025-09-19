using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        
        Task CreateAsync(ProfileRequest request);
        Task<PaginatedResponse<ProfileResponse>> GetAsync(ProfilePaginatedRequest? request);
        Task<ProfileResponse> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, ProfileRequest request);
    }
}