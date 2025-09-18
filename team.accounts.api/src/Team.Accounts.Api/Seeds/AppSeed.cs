using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Enums;
using Team.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Team.Accounts.Api.Seeds
{
    public class AppSeed
    {
        public static void Seeder(AccountsContext context){

            var apps = new List<App>();
            
            apps.Add(new App{
                Code = 1,
                Type = AppTypeEnum.Api,
                Name = "Team accounts - Api",
                Slug = "team-accounts-api",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Code = 2,
                Type = AppTypeEnum.Api,
                Name = "Team money - Api",
                Slug = "team-money-api",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Code = 3,
                Type = AppTypeEnum.Api,
                Name = "Team task - Api",
                Slug = "team-task-api",
                Status = StatusEnum.Active
            });
            
            apps.Add(new App{
                Code = 4,
                Type = AppTypeEnum.Web,
                Name = "Team accounts - Management",
                Slug = "team-accounts-management",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Code = 5,
                Type = AppTypeEnum.Api,
                Name = "Team file - API",
                Slug = "team-file-api",
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