using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Accounts.Login.Infra.Repositories;
using Accounts.Login.Infra.Repositories.Interfaces;

namespace Accounts.Login.Web.Configurations
{
    public static class DependenceConfiguration
    {
        public static void AddDependence(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<JwtSecurityTokenHandler>();
            builder.Services.AddScoped<IClientAuthorizationRepository, ClientAuthorizationRepository>();
            builder.Services.AddScoped<IUserAuthorizationRepository, UserAuthorizationRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
            builder.Services.AddScoped<IAppRepository, AppRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
        }
    }
}