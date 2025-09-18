using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;

namespace Team.Accounts.Core.Handlers
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