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
                Name = "Family accounts",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Name = "Family money",
                Status = StatusEnum.Active
            });

            apps.Add(new App{
                Name = "Family task",
                Status = StatusEnum.Active
            });

            var appsDb = context.Apps.AsNoTracking().ToList();

            foreach(var app in apps)
            {
                if(!appsDb.Any(w => w.Name == app.Name))
                    context.Apps.Add(app);
            }

            context.SaveChanges();
        }
    }
}