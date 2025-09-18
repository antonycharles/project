using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
{
    public interface IUserProfileHandler
    {
        Task<UserProfile> CreateAsync(UserProfileRequest request);
        Task DeleteAsync(Guid id);
    }
}