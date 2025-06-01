using Family.Accounts.Application.Providers;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Application.Handlers
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
                .FirstOrDefaultAsync(w => (w.Email == request.Email && request.UserId == null) || (w.Id == request.UserId && request.Email == null));

            if(user == null)
                throw new BusinessException(MSG_USER_OR_PASSAWORD_INVALID);

            var passwordHash = request.Password != null ? _passwordProvider.HashPassword(request.Password) : Guid.NewGuid().ToString();

            if(user.Password != passwordHash && request.UserId != user.Id)
                throw new BusinessException(MSG_USER_OR_PASSAWORD_INVALID);

            if(user.Status != StatusEnum.Active || user.IsDeleted == true)
                throw new BusinessException(MSG_USER_INACTIVE);

            if(request.AppSlug != null)
            {
                var userProfile = await GetUserProfileAsync(user,request);
                user.UserProfiles.Add(userProfile);
            }

            return _tokenHandler.GenerateToken(user,request);
        }

        private async Task<UserProfile> GetUserProfileAsync(User user, UserAuthenticationRequest request)
        {
            var userProfileExist = user.UserProfiles
                .FirstOrDefault(w => w.Profile.App.Slug == request.AppSlug);
                
            if(userProfileExist != null)
                return userProfileExist;

            var profileDefault = await _context.Profiles.AsNoTracking()
                .Include(i => i.App)
                .FirstOrDefaultAsync(w => w.IsDefault == true && w.App.Slug == request.AppSlug);

            if(profileDefault == null)
                throw new BusinessException(MSG_PROFILE_NOT_FOUND_FOR_USER);

            _context.UserProfiles.Add(new UserProfile{
                UserId = user.Id,
                ProfileId = profileDefault.Id
            });

            await _context.SaveChangesAsync();

            var userProfile = await _context.UserProfiles.AsNoTracking()
                .Include(i => i.Profile)
                .ThenInclude(i => i.App)
                .FirstOrDefaultAsync(w => w.Profile.App.Slug == request.AppSlug);

            if(userProfile == null)
                throw new BusinessException(MSG_PROFILE_NOT_FOUND_FOR_USER);

            return userProfile;
        }
    }
}