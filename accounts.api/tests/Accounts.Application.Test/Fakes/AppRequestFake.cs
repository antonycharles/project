using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Accounts.Core.Enums;
using Accounts.Core.Requests;

namespace Accounts.Application.Test.Fakes
{
    public static class AppRequestFake
    {
        public static Faker<AppRequest> Create(){
            return new Faker<AppRequest>()
                .RuleFor(r => r.Name, r => r.Name.FullName())
                .RuleFor(r => r.Status, r => r.PickRandom<StatusEnum>());
        }
    }
}