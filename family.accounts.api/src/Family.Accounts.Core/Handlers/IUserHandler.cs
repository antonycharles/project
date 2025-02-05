using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;

namespace Family.Accounts.Core.Handlers
{
    public interface IUserHandler
    {
        Task<PaginatedResponse<UserResponse>> GetAsync(PaginatedRequest request);
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> CreateAsync(UserRequest request);
        Task UpdateAsync(Guid id, UserUpdateRequest request);
        Task DeleteAsync(Guid id);
    }
}