using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Settings;

namespace Family.Accounts.Login.Web.Configurations
{
    public static class ConfigurationRoot
    {
        public static void AddConfigurationRoot(this WebApplicationBuilder builder)
        {
            #if RELEASE
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("secrets/appsettings.json", false, true);
            #else
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"src/Family.Accounts.Login.Web/appsettings.json", false, true);
            #endif

            builder.Configuration.AddEnvironmentVariables();

            builder.Services
                .AddOptions<AccountsLoginSettings>()
                .BindConfiguration(nameof(AccountsLoginSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart(); 
        }

        public static AccountsLoginSettings GetSettings(this WebApplicationBuilder builder)
        {
            return builder.Configuration
                .GetSection(nameof(AccountsLoginSettings))
                .Get<AccountsLoginSettings>();
        }
    }
}