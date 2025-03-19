using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class UserProfileSeed
    {
        public static void Seeder(AccountsContext context){
            SeederFamilyAccountsAdmin(context);

            context.SaveChanges();
        } 


        private static void SeederFamilyAccountsAdmin(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "family-accounts-api");

            if(app == null)
                return;

            var profile = context.Profiles.AsNoTracking().FirstOrDefault(w => w.Slug == "admin");

            if(profile == null)
                return;

            var permissions = context.Permissions.AsNoTracking().Where(w => w.AppId == app.Id).ToList();

            foreach(var permission in permissions)
            {
                var profilePermission = context.ProfilePermissions.AsNoTracking()
                    .FirstOrDefault(w => w.ProfileId == profile.Id && w.PermissionId == permission.Id);

                if(profilePermission == null)
                    context.ProfilePermissions.Add(new ProfilePermission { ProfileId = profile.Id, PermissionId = permission.Id });
            }
        }
    }
}