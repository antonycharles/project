using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Settings;

namespace Accounts.Management.Web.Configurations
{
    public static class ConfigurationRoot
    {
        public static void AddConfigurationRoot(this WebApplicationBuilder builder)
        {
            #if RELEASE
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("secrets/appsettings.json", false, true);
            #else
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"appsettings.json", false, true);
            #endif

            builder.Configuration.AddEnvironmentVariables();

            builder.Services
                .AddOptions<AccountsManagementSettings>()
                .BindConfiguration(nameof(AccountsManagementSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart(); 
        }

        public static AccountsManagementSettings GetSettings(this WebApplicationBuilder builder)
        {
            return builder.Configuration
                .GetSection(nameof(AccountsManagementSettings))
                .Get<AccountsManagementSettings>();
        }
    }
}