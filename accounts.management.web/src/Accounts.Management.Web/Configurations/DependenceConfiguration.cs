using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Refits;
using Accounts.Management.Infrastructure.Repositories;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Settings;
using Accounts.Management.Web.Handlers;
using Accounts.Management.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Refit;

namespace Accounts.Management.Web.Configurations
{
    public static class DependenceConfiguration
    {
        public static void AddDependence(this WebApplicationBuilder builder, AccountsManagementSettings settings)
        {
            builder.Services.AddScoped<AccountsApiAuthHeaderHandler>();
            builder.Services.AddScoped<FileApiAuthHeaderHandler>();
            builder.Services.AddScoped<IClaimsTransformation, ClaimsTranformer>();
            builder.Services.AddScoped<IAppRepository, AppRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();

            builder.Services.AddRefitClient<IAppRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(settings.AccountsApiUrl);
            })
            .AddHttpMessageHandler<AccountsApiAuthHeaderHandler>();

            builder.Services.AddRefitClient<IProfileRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(settings.AccountsApiUrl);
            })
            .AddHttpMessageHandler<AccountsApiAuthHeaderHandler>();

            builder.Services.AddRefitClient<IPermissionRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(settings.AccountsApiUrl);
            })
            .AddHttpMessageHandler<AccountsApiAuthHeaderHandler>();

            builder.Services.AddRefitClient<IUserRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(settings.AccountsApiUrl);
            })
            .AddHttpMessageHandler<AccountsApiAuthHeaderHandler>();


            builder.Services.AddRefitClient<IClientRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(settings.AccountsApiUrl);
            })
            .AddHttpMessageHandler<AccountsApiAuthHeaderHandler>();

            builder.Services.AddRefitClient<IClientAuthorizationRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(settings.AccountsApiUrl);
            });

            builder.Services.AddRefitClient<ILoginRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(settings.AccountsLoginUrl);
            });
            

            builder.Services.AddRefitClient<IFileRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new  Uri(settings.FileApiUrl);
            })
            .AddHttpMessageHandler<FileApiAuthHeaderHandler>();
        }
    }
}