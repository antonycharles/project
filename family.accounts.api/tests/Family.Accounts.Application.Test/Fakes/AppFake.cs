using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;

namespace Family.Accounts.Application.Test.Fakes
{
    public static class AppFake
    {
        public static Faker<App> Create(){
            return new Faker<App>()
                .RuleFor(r => r.Id, r => Guid.NewGuid())
                .RuleFor(r => r.Name, r => r.Name.FullName())
                .RuleFor(r => r.Status, r => r.PickRandom<StatusEnum>());
        }
    }
}