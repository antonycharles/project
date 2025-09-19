using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IUserPhotoHandler
    {
        Task<UserPhotoResponse> GetByIdAsync(Guid id);
        Task UpdateOrCreateAsync(UserPhotoRequest request);
        Task DeleteAsync(Guid id);
    }
}