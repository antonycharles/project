using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Api.Helpers;
using Family.Accounts.Core.Entities;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class ProfilePermissionSeed
    {
        public static void Seeder(AccountsContext context){
            SeederFamilyAccountsApiAdmin(context);
            SeederFamilyAccountsApiLogin(context);
            SeederFamilyAccountsManagementLogin(context);

            context.SaveChanges();
        }

        private static void SeederFamilyAccountsManagementLogin(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "family-accounts-management");

            if(app == null)
                return;

            var profile = context.Profiles.AsNoTracking()
                .FirstOrDefault(w => w.Slug == "admin" && w.AppId == app.Id);

            if(profile == null)
                return;

            var permissions = context.Permissions.AsNoTracking().Where(w => w.AppId == app.Id).ToList();

            AddProfilePermission(context, profile, permissions);
        }

        private static void SeederFamilyAccountsApiLogin(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "family-accounts-api");

            if(app == null)
                return;

            var profile = context.Profiles.AsNoTracking()
                .FirstOrDefault(w => w.Slug == "login" && w.AppId == app.Id);

            if(profile == null)
                return;

            var roles = new List<string>
            {
                "user-authorization",
                "token-public-key",
                "user-create",
                "user-update",
            };

            var permissions = context.Permissions.AsNoTracking()
                .Where(w => w.AppId == app.Id && roles.Contains(w.Role)).ToList();

            AddProfilePermission(context, profile, permissions);
        }

        private static void SeederFamilyAccountsApiAdmin(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "family-accounts-api");

            if (app == null)
                return;

            var profile = context.Profiles.AsNoTracking().FirstOrDefault(w => w.Slug == "admin" && w.AppId == app.Id);

            if (profile == null)
                return;

            var rolesIgnore = new List<string>
            {
                "user-authorization",
            };

            var permissions = context.Permissions.AsNoTracking()
                .Where(w => w.AppId == app.Id && rolesIgnore.Contains(w.Role) == false)
                .ToList();

            AddProfilePermission(context, profile, permissions);
        }


        private static void AddProfilePermission(AccountsContext context, Profile profile, List<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                var profilePermission = context.ProfilePermissions.AsNoTracking()
                    .FirstOrDefault(w => w.ProfileId == profile.Id && w.PermissionId == permission.Id);

                if (profilePermission == null)
                    context.ProfilePermissions.Add(new ProfilePermission { ProfileId = profile.Id, PermissionId = permission.Id });
            }
        }
    }
}