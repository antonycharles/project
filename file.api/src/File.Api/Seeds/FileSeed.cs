using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Infrastructure.Entities;
using File.Infrastructure.interfaces;

namespace File.Api.Seeds
{
    public class FileSeed
    {
        public static void Seeder(IFileDocumentRepository fileDocumentRepository)
        {
            var appId = Guid.Parse("38eef297-775c-4e25-a3ba-9b4b1c217657");

            var lista = new List<FileDocument>();

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("6c67adb2-5cb9-4af9-b30a-9570c3c23e96"),
                AppId = appId,
                Name = "favicon.png",
                Url = "http://localhost:9502/File/6c67adb2-5cb9-4af9-b30a-9570c3c23e96",
                Path = "Uploads/base/imgs/favicon.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("11bef12b-d527-4e33-88aa-ec1c9028917a"),
                AppId = appId,
                Name = "favicon-purple.png",
                Url = "http://localhost:9502/File/11bef12b-d527-4e33-88aa-ec1c9028917a",
                Path = "Uploads/base/imgs/favicon-purple.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("2e589bd8-3efc-4ba0-9332-012b6cfec344"),
                AppId = appId,
                Name = "logo-project-sync.png",
                Url = "http://localhost:9502/File/2e589bd8-3efc-4ba0-9332-012b6cfec344",
                Path = "Uploads/base/imgs/logo-project-sync.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("c6b7bd5e-a0b1-4012-8ac6-e8103e07d72e"),
                AppId = appId,
                Name = "logo-project-sync-purple.png",
                Url = "http://localhost:9502/File/c6b7bd5e-a0b1-4012-8ac6-e8103e07d72e",
                Path = "Uploads/base/imgs/logo-project-sync-purple.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("7a6e2946-c0cb-4c6f-96ec-4753f33a5d6a"),
                AppId = appId,
                Name = "logo-project-sync-white.png",
                Url = "http://localhost:9502/File/7a6e2946-c0cb-4c6f-96ec-4753f33a5d6a",
                Path = "Uploads/base/imgs/logo-project-sync-white.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("b23df07a-ba3a-4c4a-9881-d44f36dec773"),
                AppId = appId,
                Name = "android-chrome-192x192.png",
                Url = "http://localhost:9502/File/b23df07a-ba3a-4c4a-9881-d44f36dec773",
                Path = "Uploads/base/imgs/favicon_io/android-chrome-192x192.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("f7064f56-e9fd-441e-b4d9-41cda47787a7"),
                AppId = appId,
                Name = "android-chrome-512x512.png",
                Url = "http://localhost:9502/File/f7064f56-e9fd-441e-b4d9-41cda47787a7",
                Path = "Uploads/base/imgs/favicon_io/android-chrome-512x512.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("0eea3ce7-5933-4817-8028-7caa33bab297"),
                AppId = appId,
                Name = "apple-touch-icon.png",
                Url = "http://localhost:9502/File/0eea3ce7-5933-4817-8028-7caa33bab297",
                Path = "Uploads/base/imgs/favicon_io/apple-touch-icon.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("30d37a14-2019-49d1-8ef9-c22d3bc8ecbf"),
                AppId = appId,
                Name = "favicon-16x16.png",
                Url = "http://localhost:9502/File/30d37a14-2019-49d1-8ef9-c22d3bc8ecbf",
                Path = "Uploads/base/imgs/favicon_io/favicon-16x16.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("de1580e3-d49c-4d56-a099-66dadd4b169a"),
                AppId = appId,
                Name = "favicon-32x32.png",
                Url = "http://localhost:9502/File/de1580e3-d49c-4d56-a099-66dadd4b169a",
                Path = "Uploads/base/imgs/favicon_io/favicon-32x32.png",
                ContentType = "image/png",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            lista.Add(new FileDocument
            {
                Id = Guid.Parse("011606fe-8a22-4bde-a086-d17ef30d7ea6"),
                AppId = appId,
                Name = "favicon.ico",
                Url = "http://localhost:9502/File/011606fe-8a22-4bde-a086-d17ef30d7ea6",
                Path = "Uploads/base/imgs/favicon_io/favicon.ico",
                ContentType = "image/x-icon",
                Size = 49000,
                IsPublic = true,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            foreach (var item in lista)
            {
                var existing = fileDocumentRepository.GetByIdAsync(item.Id).Result;
                if (existing == null)
                {
                    fileDocumentRepository.AddAsync(item).Wait();
                }
            }
        }
    }
}