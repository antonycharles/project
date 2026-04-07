using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Api.Helpers;
using File.Api.Seeds;
using File.Infrastructure.interfaces;
using File.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;

namespace File.Api.Configurations
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

        public static async Task SeedDataAsync(this IHost host,FileSettings settings){
            using(var scope = host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IFileDocumentRepository>();
                var uploadHelper = scope.ServiceProvider.GetService<IUploadHelper>();
                var environment = scope.ServiceProvider.GetService<IWebHostEnvironment>();

                if (repository == null || uploadHelper == null || environment == null)
                    return;

                await FileSeed.SeederAsync(repository, uploadHelper, environment, settings);
            }
        }
    }
}
