using Family.Accounts.Application.Providers;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Application.Handlers
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        private const string MSG_USER_OR_PASSAWORD_INVALID = "User or password invalid";
        private const string MSG_PROFILE_NOT_FOUND_FOR_USER = "Profile not found for user";

        private readonly AccountsContext _context;
        private readonly IPasswordProvider _passwordProvider;

        public AuthenticationHandler(
            AccountsContext context,
            PasswordProvider passwordProvider){
            _context = context;
            _passwordProvider = passwordProvider;
        }

        public async Task<AuthenticationResponse> AuthenticationAsync(UserAuthenticationRequest request)
        {

            var userDb = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Email == request.Email && w.Status == StatusEnum.Active);

            if(userDb == null)
                throw new BusinessException(MSG_USER_OR_PASSAWORD_INVALID);

            var passwordHash = _passwordProvider.HashPassword(request.Password);

            if(userDb == null || userDb.Password != passwordHash)
                throw new BusinessException(MSG_USER_OR_PASSAWORD_INVALID);

            return new AuthenticationResponse();
        }
    }
}