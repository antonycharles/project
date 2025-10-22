using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Providers;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Api.Seeds
{
    public class UserAccountsSeed
    {
        public static void Seeder(AccountsContext context, IPasswordProvider passwordProvider)
        {
            AddAdmin(context, passwordProvider);
        }

        private static void AddAdmin(AccountsContext context, IPasswordProvider passwordProvider)
        {

            var company = new Company
            {
                Id = new Guid("a1b2c3d4-e5f6-4789-0abc-def123456789"),
                Name = "Project Team",
                Status = StatusEnum.Active
            };

            if (!context.Companies.AsNoTracking().Any(w => w.Id == company.Id))
                context.Companies.Add(company);


            var user = new User
            {
                Id = new Guid("d3b0f1a2-4c5e-4b8c-9f7e-1a2b3c4d5e6f"),
                Name = "User admin",
                Email = "user.admin@team.com",
                Password = passwordProvider.HashPassword("123456"),
                LastCompanyId = company.Id,
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
                    ProfileId = context.Profiles.AsNoTracking().FirstOrDefault(w => w.Slug == "admin" && w.App.Slug == "accounts-management").Id,
                    CompanyId = company.Id,
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