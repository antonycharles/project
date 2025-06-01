using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Repositories;

namespace Family.Accounts.Login.Web.Configurations
{
    public static class DependenceConfiguration
    {
        public static void AddDependence(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IClientAuthorizationRepository, ClientAuthorizationRepository>();
            builder.Services.AddScoped<IUserAuthorizationRepository, UserAuthorizationRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
        }
    }
}