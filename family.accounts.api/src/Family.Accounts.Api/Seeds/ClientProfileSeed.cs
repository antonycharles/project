using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class ClientProfileSeed
    {
        public static void Seeder(AccountsContext context){
            var clientProfiles = new List<ClientProfile>();
            
            clientProfiles.Add(new ClientProfile{
                ClientId = new Guid("e7f8a9b0-1c2d-3e4f-5a6b-7c8d9e0f1a2b"),
                ProfileId =  context.Profiles.AsNoTracking().FirstOrDefault(w => w.Slug == "admin").Id
            });

            var clientProfileDb = context.ClientProfiles.AsNoTracking().ToList();

            foreach(var clientProfile in clientProfiles)
            {
                var exist = clientProfileDb.Any(w => w.ClientId == clientProfile.ClientId && w.ProfileId == clientProfile.ProfileId);
                
                if(exist == false)
                    context.ClientProfiles.Add(clientProfile);
            }

            context.SaveChanges();
        }
    }
}