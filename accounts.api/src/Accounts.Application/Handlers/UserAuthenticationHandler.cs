using Accounts.Application.Providers;
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
    public class UserAuthorizationHandler : IUserAuthorizationHandler
    {
        private const string MSG_USER_OR_PASSAWORD_INVALID = "User or password invalid";
        private const string MSG_PROFILE_NOT_FOUND_FOR_USER = "Profile not found for user and app";
        private const string MSG_USER_INACTIVE = "User is inactive";

        private readonly AccountsContext _context;
        private readonly IPasswordProvider _passwordProvider;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserHandler _userHandler;

        public UserAuthorizationHandler(
            AccountsContext context,
            IPasswordProvider passwordProvider,
            ITokenHandler tokenHandler,
            IUserHandler userHandler){    
            _context = context;
            _passwordProvider = passwordProvider;
            _tokenHandler = tokenHandler;
            _userHandler = userHandler;
        }

        public async Task<AuthenticationResponse> AuthenticationAsync(UserAuthenticationRequest request)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.App)
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.ProfilePermissions.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Permission)
                .FirstOrDefaultAsync(w => w.Email == request.Email);

            if(user == null)
                throw new BusinessException(MSG_USER_OR_PASSAWORD_INVALID);

            var passwordHash = request.Password != null ? _passwordProvider.HashPassword(request.Password) : Guid.NewGuid().ToString();

            if(user.Password != passwordHash)
                throw new BusinessException(MSG_USER_OR_PASSAWORD_INVALID);

            if(user.Status != StatusEnum.Active || user.IsDeleted == true)
                throw new BusinessException(MSG_USER_INACTIVE);

            if(request.AppSlug != null)
            {
                var userProfile = await GetUserProfileAsync(user,request);
                user.UserProfiles.Add(userProfile);
            }

            return await _tokenHandler.GenerateUserTokenAsync(user.Id, request.AppSlug);
        }

        private async Task<UserProfile> GetUserProfileAsync(User user, UserAuthenticationRequest request)
        {
            if(user.LastCompanyId == null)
            {
                user.LastCompanyId = _context.UserProfiles.AsNoTracking()
                    .Where(w => w.UserId == user.Id && w.Profile.App.Slug == request.AppSlug)
                    .Select(s => s.CompanyId)
                    .FirstOrDefault();
            }

            var userProfileExist = user.UserProfiles
                .FirstOrDefault(w => w.Profile.App.Slug == request.AppSlug && w.CompanyId == user.LastCompanyId);
                
            if(userProfileExist != null)
                return userProfileExist;

            var profileDefault = await _context.Profiles.AsNoTracking()
                .Include(i => i.App)
                .FirstOrDefaultAsync(w => w.IsDefault == true && w.App.Slug == request.AppSlug);

            if(profileDefault == null || user.LastCompanyId == null)
                throw new BusinessException(MSG_PROFILE_NOT_FOUND_FOR_USER);

            var userProfile = new UserProfile{
                UserId = user.Id,
                CompanyId = user.LastCompanyId.Value,
                ProfileId = profileDefault.Id
            };

            _context.UserProfiles.Add(userProfile);

            await _context.SaveChangesAsync();

            if(userProfile == null)
                throw new BusinessException(MSG_PROFILE_NOT_FOUND_FOR_USER);

            return userProfile;
        }
    }
}