using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Core.Handlers
{
    public interface IUserHandler
    {
        Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest request);
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> CreateAsync(UserRequest request);
        Task UpdateAsync(Guid id, UserUpdateRequest request);
        Task UpdateLastCompanyAsync(Guid id, Guid companyId);
        Task DeleteAsync(Guid id);
    }
}