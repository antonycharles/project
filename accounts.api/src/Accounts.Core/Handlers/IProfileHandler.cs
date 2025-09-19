using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IProfileHandler
    {
        Task<PaginatedResponse<ProfileResponse>> GetAsync(PaginatedProfileRequest request);
        Task<ProfileResponse> GetByIdAsync(Guid id);
        Task<ProfileResponse> CreateAsync(ProfileRequest request);
        Task UpdateAsync(Guid id, ProfileRequest request);
        Task UpdatePermissionsAsync(Guid id, Guid[]? permissions);
        Task DeleteAsync(Guid id);
    }
}