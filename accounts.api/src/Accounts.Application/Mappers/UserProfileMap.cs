using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Application.Mappers
{
    public static class UserProfileMap
    {
        public static UserProfile ToUserProfile(this UserProfileRequest request) => new UserProfile
        {
            UserId = request.UserId,
            ProfileId = request.ProfileId,
            CompanyId = request.CompanyId
        };
        
        public static UserProfileResponse ToUserProfileResponse(this UserProfile entity) => new UserProfileResponse
        {
            UserId = entity.UserId,
            ProfileId = entity.ProfileId,
            Profile = entity.Profile != null ? entity.Profile.ToProfileResponse() : null,
            CompanyId = entity.CompanyId,
            Company = entity.Company != null ? entity.Company.ToCompanyResponse() : null
        };
    }
}