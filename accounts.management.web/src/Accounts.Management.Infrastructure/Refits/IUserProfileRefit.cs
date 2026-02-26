using Accounts.Management.Infrastructure.Requests;
using Refit;
using System;
using System.Threading.Tasks;

namespace Accounts.Management.Infrastructure.Refits
{
    public interface IUserProfileRefit
    {
        [Post("/UserProfile")]
        Task CreateAsync(UserProfileRequest request);

        [Put("/UserProfile/{id}")]
        Task UpdateAsync(Guid id, UserProfileUpdateRequest request);

        [Delete("/UserProfile/{id}")]
        Task DeleteAsync(Guid id);
    }
}
