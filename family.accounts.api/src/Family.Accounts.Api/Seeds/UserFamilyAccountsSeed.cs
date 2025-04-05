using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Application.Providers;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class UserFamilyAccountsSeed
    {
        public static void Seeder(AccountsContext context, IPasswordProvider passwordProvider)
        {
            AddAdmin(context, passwordProvider);
        }

        private static void AddAdmin(AccountsContext context, IPasswordProvider passwordProvider)
        {
            var user = new User
            {
                Id = new Guid("d3b0f1a2-4c5e-4b8c-9f7e-1a2b3c4d5e6f"),
                Name = "User admin",
                Email = "user.admin@family.com",
                Password = passwordProvider.HashPassword("123456"),
                Status = StatusEnum.Active
            };

            if (!context.Users.AsNoTracking().Any(w => w.Id == user.Id))
                context.Users.Add(user);


            var userProfiles = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ProfileId = context.Profiles.AsNoTracking().FirstOrDefault(w => w.Slug == "admin" && w.App.Slug == "family-accounts-api").Id,
                    Status = StatusEnum.Active
                }
            };
            
            foreach (var userProfile in userProfiles)
            {
                if (!context.UserProfiles.AsNoTracking().Any(w => w.UserId == userProfile.UserId && w.ProfileId == userProfile.ProfileId))
                    context.UserProfiles.Add(userProfile);
            }

            context.SaveChanges();
        }
    }
}