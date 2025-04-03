using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Application.Providers;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Api.Seeds
{
    public class ClientSeed
    {
        public static void Seeder(AccountsContext context, IPasswordProvider passwordProvider){
            var clients = new List<Client>();

            clients.Add(new Client{
                Id = new Guid("d3b0f1a2-4c5e-4b8c-9f7e-1a2b3c4d5e6f"),
                Name = "Family accounts - Login",
                Password = passwordProvider.HashPassword("123456"),
                Status = StatusEnum.Active
            });

            clients.Add(new Client{
            Id = new Guid("e7f8a9b0-1c2d-3e4f-5a6b-7c8d9e0f1a2b"),
                Name = "Family accounts - Admin",
                Password = passwordProvider.HashPassword("123456"),
                Status = StatusEnum.Active
            });


            var clientsDb = context.Clients.AsNoTracking().ToList();

            foreach(var client in clients)
            {
                if(!clientsDb.Any(w => w.Id == client.Id))
                    context.Clients.Add(client);
            }

            context.SaveChanges();
        }
    }
}