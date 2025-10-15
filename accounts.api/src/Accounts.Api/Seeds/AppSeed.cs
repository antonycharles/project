using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Api.Seeds
{
    public class AppSeed
    {
        public static void Seeder(AccountsContext context){

            var apps = new List<App>();
            
            apps.Add(new App{
                Code = 1,
                Type = AppTypeEnum.Api,
                Name = "accounts - Api",
                Slug = "accounts-api",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Code = 2,
                Type = AppTypeEnum.Api,
                Name = "money - Api",
                Slug = "money-api",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Code = 3,
                Type = AppTypeEnum.Api,
                Name = "task - Api",
                Slug = "task-api",
                Status = StatusEnum.Active
            });
            
            apps.Add(new App{
                Code = 4,
                Type = AppTypeEnum.Web,
                Name = "accounts - Management",
                Slug = "accounts-management",
                Status = StatusEnum.Active
            });

            apps.Add(new App
            {
                Code = 5,
                Type = AppTypeEnum.Api,
                Name = "file - API",
                Slug = "file-api",
                Status = StatusEnum.Active
            });


            apps.Add(new App
            {
                Code = 6,
                Type = AppTypeEnum.Api,
                Name = "Notification - Api",
                Slug = "notification-api",
                Status = StatusEnum.Active
            });
            
            apps.Add(new App{
                Code = 7,
                Type = AppTypeEnum.Api,
                Name = "Project - API",
                Slug = "project-api",
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