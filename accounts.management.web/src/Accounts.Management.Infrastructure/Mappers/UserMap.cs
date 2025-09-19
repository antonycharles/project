using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Requests;
using Accounts.Management.Infrastructure.Responses;

namespace Accounts.Management.Infrastructure.Mappers
{
    public static class UserMap
    {
        
        public static UserRequest ToUserRequest(this UserResponse user) => new UserRequest{
            Name = user.Name,
            Email = user.Email,
            Status = user.Status,
        };
        
    }
}