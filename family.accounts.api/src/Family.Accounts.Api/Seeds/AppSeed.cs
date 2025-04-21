using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class AppSeed
    {
        public static void Seeder(AccountsContext context){

            var apps = new List<App>();
            
            apps.Add(new App{
                Code = 1,
                Type = AppTypeEnum.Api,
                Name = "Family accounts - Api",
                Slug = "family-accounts-api",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Code = 2,
                Type = AppTypeEnum.Api,
                Name = "Family money - Api",
                Slug = "family-money-api",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Code = 3,
                Type = AppTypeEnum.Api,
                Name = "Family task - Api",
                Slug = "family-task-api",
                Status = StatusEnum.Active
            });
            
            apps.Add(new App{
                Code = 4,
                Type = AppTypeEnum.Web,
                Name = "Family accounts - Management",
                Slug = "family-accounts-management",
                Status = StatusEnum.Active
            });

            var appsDb = context.Apps.AsNoTracking().ToList();

            foreach(var app in apps)
            {
                if(!appsDb.Any(w => w.Slug == app.Slug))
                    context.Apps.Add(app);
            }

            context.SaveChanges();
        }
    }
}