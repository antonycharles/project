using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Api.Helpers;
using File.Infrastructure.Entities;
using File.Infrastructure.interfaces;
using File.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace File.Api.Seeds
{
    public class FileSeed
    {

        public static async Task SeederAsync(
            IFileDocumentRepository fileDocumentRepository,
            IUploadHelper uploadHelper,
            IWebHostEnvironment webHostEnvironment,
            FileSettings settings)
        {
            var appAccountsManagementId = Guid.Parse("4fef2c18-c5ae-4cf7-a5d8-15d5db395c37");
            
            await SeedIconesAsync(
                fileDocumentRepository, 
                uploadHelper, 
                webHostEnvironment, 
                new Guid("71402371-2fc0-4e15-b9e0-384f45508afb"),
                appAccountsManagementId,
                "project-web-icone.png",
                "image/png",
                settings.BaseUrl);

            await SeedIconesAsync(
                fileDocumentRepository, 
                uploadHelper, 
                webHostEnvironment, 
                new Guid("d8e6b464-3b70-42f3-8bd4-7f77c8ca5189"),
                appAccountsManagementId,
                "project-management-web.png",
                "image/png",
                settings.BaseUrl);
        }

        private static async Task SeedIconesAsync(
            IFileDocumentRepository fileDocumentRepository,
            IUploadHelper uploadHelper,
            IWebHostEnvironment webHostEnvironment,
            Guid documentId,
            Guid appId,
            string fileName,
            string contentType = "image/png",
            string urlBase = "http://localhost:9502/")
        {

            var existing = await fileDocumentRepository.GetByAppIdAndNameAsync(appId, fileName);
            if (existing != null)
                return;

            var sourcePath = Path.Combine(webHostEnvironment.WebRootPath, "imgs", "icones", fileName);
            if (!System.IO.File.Exists(sourcePath))
                return;

            await using var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormFile formFile = new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            var document = await uploadHelper.UploadFileAsync(formFile, appId.ToString(), urlBase);
            document.AppId = appId;
            document.IsPublic = true;
            document.Id = documentId;

            await fileDocumentRepository.AddAsync(document);
        }
    }
}
