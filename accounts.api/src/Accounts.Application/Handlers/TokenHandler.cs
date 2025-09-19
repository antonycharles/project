using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Accounts.Application.Extensions;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Core.Handlers;
using Accounts.Core.Requests;
using Accounts.Core.Responses;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Application.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private const string T_ISSUER = "accounts.api";

        private readonly AccountsContext _context;
        private readonly ITokenKeyHandler _tokenKeyHandler;

        public TokenHandler(ITokenKeyHandler tokenKeyHandler, AccountsContext context)
        {
            _tokenKeyHandler = tokenKeyHandler;
            _context = context;
        }

        public async Task<AuthenticationResponse> GenerateClientTokenAsync(Guid clientId, string appSlug)
        {
            var client = await _context.Clients.AsNoTracking()
                .Include(i => i.ClientProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.ProfilePermissions.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Permission)
                .Include(i => i.ClientProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.App)
                .FirstOrDefaultAsync(w => w.Id == clientId);

            client.ClientProfiles = client.ClientProfiles.Where(w => w.Profile.App.Slug == appSlug).ToList();

            var profile = client.ClientProfiles
                .Select(s => s.Profile).FirstOrDefault();

            var roles = GetRoles(profile);

            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Id, client.Id.ToString()),
                new Claim(CustomClaimTypes.Name, client.Name),
                new Claim(CustomClaimTypes.Type, UserTypeEnum.client.ToString()),
            };

            foreach (var role in roles)
                claims.Add(new Claim(CustomClaimTypes.Role, profile.App.Code + "-" + role));


            return GenerationAuthenticationResponse(claims, client.Id, UserTypeEnum.client, profile.App);
        }


        public async Task<AuthenticationResponse> GenerateUserTokenAsync(Guid userId, string appSlug)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.App)
                .Include(i => i.UserProfiles.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Profile)
                .ThenInclude(i => i.ProfilePermissions.Where(w => w.Status == StatusEnum.Active && w.IsDeleted == false))
                .ThenInclude(i => i.Permission)
                .FirstOrDefaultAsync(w => w.Id == userId);

            var profile = user?.UserProfiles?
                .Where(w => w.Status == StatusEnum.Active)
                .Where(w => w.Profile?.App?.Slug == appSlug && w.Profile.Status == StatusEnum.Active)
                .Select(s => s.Profile).FirstOrDefault();

            var roles = GetRoles(profile);

            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                new Claim(CustomClaimTypes.Name, user.Name),
                new Claim(CustomClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.Type, UserTypeEnum.user.ToString()),
            };


            foreach (var role in roles)
                claims.Add(new Claim(CustomClaimTypes.Role, profile.App.Code + "-" + role));

            
            return GenerationAuthenticationResponse(claims, user.Id, UserTypeEnum.user, profile?.App);
        }

        private AuthenticationResponse GenerationAuthenticationResponse(List<Claim> claims, Guid authId, UserTypeEnum userType, App? app)
        {
            SecurityTokenDescriptor jwt = GetSecurityTokenDescriptor(claims, app?.Slug);

            var tokenHandler = new JsonWebTokenHandler();

            var key = _tokenKeyHandler.GetPrivateKey();

            jwt.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256);
            var lastJws = tokenHandler.CreateToken(jwt);

            return new AuthenticationResponse
            {
                ExpiresIn = jwt.Expires?.AddMinutes(-3),
                CallbackUrl = app?.CallbackUrl,
                Token = lastJws,
                AuthId = authId,
                AppSlug = app?.Slug,
                UserType = userType,
                RefreshToken = Guid.NewGuid().ToString()
            };
        }


        private SecurityTokenDescriptor GetSecurityTokenDescriptor(List<Claim> claims, string? appSlug = null)
        {
            return new SecurityTokenDescriptor
            {
                Issuer = T_ISSUER,
                Audience = appSlug ?? T_ISSUER,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(2).AddMinutes(3),
                Subject = new ClaimsIdentity(claims)
            };
        }

        private List<string> GetRoles(Profile? profile)
        {
            var roles = new List<string>();

            if(profile == null)
                return roles;

            var rolesProfile = profile.ProfilePermissions
                .Where(w => w.Permission != null && w.Permission.Role != null && w.Status == StatusEnum.Active && w.Permission.Status == StatusEnum.Active)
                .Select(s => s.Permission?.Role);

            roles.AddRange(rolesProfile);

            return roles;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JsonWebTokenHandler();

            var key = _tokenKeyHandler.GetPublicKeys().First();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = T_ISSUER,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}