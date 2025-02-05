using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface IUserProfileHandler
    {
        Task<UserProfile> CreateAsync(UserProfileRequest request);
        Task DeleteAsync(Guid id);
    }
}