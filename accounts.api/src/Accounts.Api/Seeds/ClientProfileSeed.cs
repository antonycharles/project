using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Api.Seeds
{
    public class ClientProfileSeed
    {
        public static void Seeder(AccountsContext context){
            var clientProfiles = new List<ClientProfile>();
            
            clientProfiles.Add(new ClientProfile{
                ClientId = new Guid("d3b0f1a2-4c5e-4b8c-9f7e-1a2b3c4d5e6f"),
                ProfileId =  context.Profiles.AsNoTracking().FirstOrDefault(w => w.App.Slug == "accounts-api" && w.Slug == "login").Id
            });

            clientProfiles.Add(new ClientProfile{
                ClientId = new Guid("e7f8a9b0-1c2d-3e4f-5a6b-7c8d9e0f1a2b"),
                ProfileId =  context.Profiles.AsNoTracking().FirstOrDefault(w => w.App.Slug == "accounts-api" && w.Slug == "admin").Id
            });


            clientProfiles.Add(new ClientProfile{
                ClientId = new Guid("9bc91fc4-e79a-4f68-9b9a-693ddf61a7e3"),
                ProfileId =  context.Profiles.AsNoTracking().FirstOrDefault(w => w.App.Slug == "accounts-api" && w.Slug == "public-token").Id
            });

            var clientProfileDb = context.ClientProfiles.AsNoTracking().ToList();

            foreach(var clientProfile in clientProfiles)
            {
                var exist = clientProfileDb
                    .Any(w => w.ClientId == clientProfile.ClientId && w.ProfileId == clientProfile.ProfileId);
                
                if(exist == false)
                    context.ClientProfiles.Add(clientProfile);
            }

            context.SaveChanges();
        }
    }
}