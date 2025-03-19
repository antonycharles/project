using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class ProfileSeed
    {
        
        public static void Seeder(AccountsContext context){
            SeederFamilyAccounts(context);

            context.SaveChanges();
        } 

        private static void SeederFamilyAccounts(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "family-accounts-api");

            if(app == null)
                return;

            var profiles = new List<Profile>();

            profiles.Add(new Profile { Name = "Admin", AppId = app.Id, Slug = "admin" });
            profiles.Add(new Profile { Name = "User", AppId = app.Id, Slug = "user", Type = Core.Enums.ProfileTypeEnum.User, IsDefault = true });

            var profilesDb = context.Profiles.AsNoTracking().ToList();

            foreach(var profile in profiles)
            {
                if(!profilesDb.Any(w => w.Slug == profile.Slug))
                    context.Profiles.Add(profile);
            }
        }
    }
}