using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class PermissionSeed
    {
        public static void Seeder(AccountsContext context){
            SeederFamilyAccounts(context);

            context.SaveChanges();
        } 


        private static void SeederFamilyAccounts(AccountsContext context)
        {
            var app = context.Apps.AsNoTracking().FirstOrDefault(w => w.Slug == "family-accounts-api");

            if (app == null)
                return;

            var permissions = new List<Permission>();

            BasicCRUD(context, app, permissions, "App", "app");
            BasicCRUD(context, app, permissions, "Client", "client");
            BasicCRUD(context, app, permissions, "Client profile", "client-profile");
            BasicCRUD(context, app, permissions, "Permission", "permission");
            BasicCRUD(context, app, permissions, "Profile", "profile");
            BasicCRUD(context, app, permissions, "User", "user");
            BasicCRUD(context, app, permissions, "User profile", "user-profile");

            var permissionsDb = context.Permissions.AsNoTracking().ToList();

            foreach(var permission in permissions)
            {
                if(!permissionsDb.Any(w => w.Role == permission.Role && w.AppId == permission.AppId))
                    context.Permissions.Add(permission);
            }
        }

        private static void BasicCRUD(AccountsContext context, App app, List<Permission> permissions, string namePermission, string rolePermission)
        {
            var permissionId = context.Permissions.FirstOrDefault(w => w.Role == rolePermission + "-list")?.Id ?? Guid.NewGuid();
            permissions.Add(new Permission { Id = permissionId, Name = namePermission + " - list", Role = rolePermission + "-list", AppId = app.Id });
            permissions.Add(new Permission { Name = namePermission + " - create", Role = rolePermission + "-create", AppId = app.Id, PermissionFatherId = permissionId });
            permissions.Add(new Permission { Name = namePermission + " - update", Role = rolePermission + "-update", AppId = app.Id, PermissionFatherId = permissionId });
            permissions.Add(new Permission { Name = namePermission + " - delete", Role = rolePermission + "-delete", AppId = app.Id, PermissionFatherId = permissionId });
        }
    }
}