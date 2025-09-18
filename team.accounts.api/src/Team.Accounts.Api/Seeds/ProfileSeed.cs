using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Team.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Team.Accounts.Api.Seeds
{
    public class ProfileSeed
    {
        
        public static void Seeder(AccountsContext context){
            SeederTeamAccountsApi(context);
            SeederTeamAccountsManagement(context);

            context.SaveChanges();
        } 

        private static void SeederTeamAccountsApi(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "team-accounts-api");

            if(app == null)
                return;

            var profiles = new List<Profile>();

            profiles.Add(new Profile { Name = "Admin", AppId = app.Id, Slug = "admin", Type = Core.Enums.ProfileTypeEnum.System });
            profiles.Add(new Profile { Name = "Login", AppId = app.Id, Slug = "login", Type = Core.Enums.ProfileTypeEnum.System });
            profiles.Add(new Profile { Name = "Public token", AppId = app.Id, Slug = "public-token", Type = Core.Enums.ProfileTypeEnum.System });

            var profilesDb = context.Profiles.AsNoTracking().Where(w => w.AppId == app.Id).ToList();

            foreach(var profile in profiles)
            {
                if(!profilesDb.Any(w => w.Slug == profile.Slug))
                    context.Profiles.Add(profile);
            }
        }

        private static void SeederTeamAccountsManagement(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "team-accounts-management");

            if(app == null)
                return;

            var profiles = new List<Profile>();

            profiles.Add(new Profile { Name = "Admin", AppId = app.Id, Slug = "admin" });
            profiles.Add(new Profile { Name = "User", AppId = app.Id, Slug = "user", IsDefault = true });

            var profilesDb = context.Profiles.AsNoTracking().Where(w => w.AppId == app.Id).ToList();

            foreach(var profile in profiles)
            {
                if(!profilesDb.Any(w => w.Slug == profile.Slug))
                    context.Profiles.Add(profile);
            }
        }
    }
}