using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Team.Accounts.Application.Test
{
    public class DatabaseContextFake
    {
        public static AccountsContext Create(){
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

                var databaseContextFake = new AccountsContext(options);
                databaseContextFake.Database.EnsureCreated();

                return databaseContextFake;
        }
    }
}