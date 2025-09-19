using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Accounts.Core.Entities;
using Accounts.Core.Enums;

namespace Accounts.Application.Test.Fakes
{
    public static class AppFake
    {
        public static Faker<App> Create(){
            return new Faker<App>()
                .RuleFor(r => r.Id, r => Guid.NewGuid())
                .RuleFor(r => r.Code, r => new Random().Next(1000, 10000))
                .RuleFor(r => r.Slug, r => r.Lorem.Slug())
                .RuleFor(r => r.Name, r => r.Name.FullName())
                .RuleFor(r => r.Status, r => r.PickRandom<StatusEnum>());
        }
    }
}