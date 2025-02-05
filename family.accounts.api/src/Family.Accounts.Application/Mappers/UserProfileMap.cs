using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Requests;

namespace Family.Accounts.Application.Mappers
{
    public static class UserProfileMap
    {
        public static UserProfile ToUserProfile(this UserProfileRequest request) => new UserProfile{
            UserId = request.UserId,
            ProfileId = request.ProfileId,
        };
    }
}