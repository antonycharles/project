using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IUserProfileHandler
    {
        Task<UserProfileResponse> CreateAsync(UserProfileRequest request);
        Task UpdateAsync(UserProfileUpdateRequest request);
        Task DeleteAsync(Guid id);
    }
}