using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Application.Mappers;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Enums;
using Team.Accounts.Core.Exceptions;
using Team.Accounts.Core.Handlers;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;
using Team.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Team.Accounts.Application.Handlers
{
    public class UserProfileHandler : IUserProfileHandler
    {
        private readonly AccountsContext _context;

        public UserProfileHandler(AccountsContext context){
            _context = context;
        }

        public async Task<UserProfile> CreateAsync(UserProfileRequest request)
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

            return userProfile;
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