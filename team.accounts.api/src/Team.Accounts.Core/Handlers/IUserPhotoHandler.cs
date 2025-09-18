using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
{
    public interface IUserPhotoHandler
    {
        Task<UserPhotoResponse> GetByIdAsync(Guid id);
        Task UpdateOrCreateAsync(UserPhotoRequest request);
        Task DeleteAsync(Guid id);
    }
}