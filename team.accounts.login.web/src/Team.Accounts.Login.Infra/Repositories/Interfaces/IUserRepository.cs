using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Requests;
using Team.Accounts.Login.Infra.Responses;

namespace Team.Accounts.Login.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponse> CreateAsync(UserRequest request);
        Task UpdateAsync(Guid userId, UserRequest request);
    }
}