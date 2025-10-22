using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Mappers;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Core.Exceptions;
using Accounts.Core.Handlers;
using Accounts.Core.Requests;
using Accounts.Core.Responses;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Application.Handlers
{
    public class UserProfileHandler : IUserProfileHandler
    {
        private readonly AccountsContext _context;

        public UserProfileHandler(AccountsContext context){
            _context = context;
        }

        public async Task<UserProfileResponse> CreateAsync(UserProfileRequest request)
        {
            var userProfile = request.ToUserProfile();

            var profile  = await _context.Profiles.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == userProfile.ProfileId);
            
            if(profile == null)
                throw new NotFoundException("Profile not found");

            var existUserProfileForApp = await _context.UserProfiles.AsNoTracking()
                .AnyAsync(w => w.IsDeleted == false && w.UserId == userProfile.UserId && w.Profile.AppId == profile.AppId);

            if(existUserProfileForApp == true)
                throw new BusinessException("User already has a profile for this app");

            _context.UserProfiles.Add(userProfile);

            await _context.SaveChangesAsync();

            return userProfile.ToUserProfileResponse();
        }

        public async Task DeleteAsync(Guid id)
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(userProfile == null)
                throw new NotFoundException("User profile not found");

            userProfile.IsDeleted = true;

            _context.Update(userProfile);

            await _context.SaveChangesAsync();
        }
    }
}