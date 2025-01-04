using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Application.Handlers;
using Family.Accounts.Core.Handlers;

namespace Family.Accounts.Api.Configurations
{
    public static class DependenceConfiguration
    {
        public static void AddDependence(this WebApplicationBuilder builder)
        {
            //GERA-COMMANDS-ADD-REPOSITORY
            builder.Services.AddTransient<IProfileHandler,ProfileHandler>();
            builder.Services.AddTransient<IAppHandler,AppHandler>();
        }
    }
}