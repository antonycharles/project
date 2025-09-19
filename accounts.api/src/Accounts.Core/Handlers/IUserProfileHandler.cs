using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IUserProfileHandler
    {
        Task<UserProfile> CreateAsync(UserProfileRequest request);
        Task DeleteAsync(Guid id);
    }
}