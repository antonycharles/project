using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Management.Infrastructure.Refits;
using Family.Accounts.Management.Infrastructure.Repositories;
using Refit;

namespace Family.Accounts.Management.Web.Configurations
{
    public static class DependenceConfiguration
    {
        public static void AddDependence(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAppRepository, AppRepository>();
            
            builder.Services.AddRefitClient<IAppRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new  Uri("http://localhost:5201/");
            });
        }
    }
}