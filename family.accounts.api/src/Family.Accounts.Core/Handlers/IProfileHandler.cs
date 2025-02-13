using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface IProfileHandler
    {
        Task<PaginatedResponse<ProfileResponse>> GetAsync(PaginatedRequest request);
        Task<ProfileResponse> GetByIdAsync(Guid id);
        Task<ProfileResponse> CreateAsync(ProfileRequest request);
        Task UpdateAsync(Guid id, ProfileRequest request);
        Task UpdatePermissionsAsync(Guid id, Guid[]? permissions);
        Task DeleteAsync(Guid id);
    }
}