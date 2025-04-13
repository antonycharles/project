using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Family.Accounts.Application.Extensions;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Family.Accounts.Application.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private const string T_ISSUER = "family.accounts.api";
        private const string T_AUDIENCE = "family.accounts.api";

        private readonly ITokenKeyHandler _tokenKeyHandler;

        public TokenHandler(ITokenKeyHandler tokenKeyHandler)
        {
            _tokenKeyHandler = tokenKeyHandler;
        }

        public AuthenticationResponse GenerateToken(Client client)
        {
            var profile = client.ClientProfiles
                .Select(s => s.Profile).FirstOrDefault();

            var roles = GetRoles(profile);

            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Id, client.Id.ToString()),
                new Claim(CustomClaimTypes.Name, client.Name),
                new Claim(CustomClaimTypes.Type, profile.Type.ToString() ?? ""),
            };

            foreach (var role in roles)
                claims.Add(new Claim(CustomClaimTypes.Role, role));


            return GenerationAuthenticationResponse(claims, profile.App);
        }


        public AuthenticationResponse GenerateToken(User user, UserAuthenticationRequest request)
        {
            var profile = user.UserProfiles
                .Where(w => w.Status == StatusEnum.Active)
                .Where(w => w.Profile.App.Slug == request.AppSlug && w.Profile.Status == StatusEnum.Active)
                .Select(s => s.Profile).FirstOrDefault();

            var roles = GetRoles(profile);

            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                new Claim(CustomClaimTypes.Name, user.Name),
                new Claim(CustomClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.Type, "user"),
            };


            foreach (var role in roles)
                claims.Add(new Claim(CustomClaimTypes.Role, role));

            
            return GenerationAuthenticationResponse(claims, profile?.App);
        }

        private AuthenticationResponse GenerationAuthenticationResponse(List<Claim> claims, App? app)
        {
            SecurityTokenDescriptor jwt = GetSecurityTokenDescriptor(claims);

            var tokenHandler = new JsonWebTokenHandler();

            var key = _tokenKeyHandler.GetPrivateKey();

            jwt.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256);
            var lastJws = tokenHandler.CreateToken(jwt);

            return new AuthenticationResponse
            {
                ExpiresIn = jwt.Expires.Value.AddMinutes(-3),
                CallbackUrl = app?.CallbackUrl,
                Token = lastJws
            };
        }


        private SecurityTokenDescriptor GetSecurityTokenDescriptor(List<Claim> claims)
        {
            return new SecurityTokenDescriptor
            {
                Issuer = T_ISSUER,
                Audience = T_AUDIENCE,
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
                .Select(s => s.Permission.Role);

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
                ValidateAudience = true,
                ValidAudience = T_AUDIENCE,
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