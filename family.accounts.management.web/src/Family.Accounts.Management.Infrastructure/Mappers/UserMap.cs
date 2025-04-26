using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Requests;
using Family.Accounts.Management.Infrastructure.Responses;

namespace Family.Accounts.Management.Infrastructure.Mappers
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