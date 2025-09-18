using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Login.Infra.Repositories;
using Team.Accounts.Login.Infra.Repositories.Interfaces;

namespace Team.Accounts.Login.Web.Configurations
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