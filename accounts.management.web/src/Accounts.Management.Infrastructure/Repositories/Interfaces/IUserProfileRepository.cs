using Accounts.Management.Infrastructure.Requests;
using System;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task CreateAsync(UserProfileRequest request);
        Task UpdateAsync(Guid id, UserProfileUpdateRequest request);
        Task DeleteAsync(Guid id);
    }
}
