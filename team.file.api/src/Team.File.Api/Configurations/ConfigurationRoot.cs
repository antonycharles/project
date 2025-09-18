using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.File.Infrastructure.Settings;

namespace Team.File.Api.Configurations
{
    public static class ConfigurationRoot
    {
        public static void AddConfigurationRoot(this WebApplicationBuilder builder)
        {
            #if RELEASE
                builder.Configuration.AddJsonFile("secrets/appsettings.json", false, true);
            #else
                builder.Configuration.AddJsonFile("appsettings.json", false, true);
            #endif

            builder.Configuration.AddEnvironmentVariables();

            builder.Services
                .AddOptions<FileSettings>()
                .BindConfiguration(nameof(FileSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart(); 
        }

        public static FileSettings GetSettings(this WebApplicationBuilder builder)
        {
            return builder.Configuration
                .GetSection(nameof(FileSettings))
                .Get<FileSettings>();
        }
    }
}