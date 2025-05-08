using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;

namespace Family.Accounts.Management.Infrastructure.Repositories
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