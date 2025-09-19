using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Requests;

namespace Accounts.Application.Mappers
{
    public static class UserProfileMap
    {
        public static UserProfile ToUserProfile(this UserProfileRequest request) => new UserProfile{
            UserId = request.UserId,
            ProfileId = request.ProfileId,
        };
    }
}