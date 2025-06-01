using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface IUserPhotoHandler
    {
        Task<UserPhotoResponse> GetByIdAsync(Guid id);
        Task UpdateOrCreateAsync(UserPhotoRequest request);
        Task DeleteAsync(Guid id);
    }
}