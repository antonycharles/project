using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Infrastructure.Entities;
using Family.File.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Family.File.Api.Helpers
{
    public class UploadHelper : IUploadHelper
    {
        private readonly FileSettings _fileSettings;

        public UploadHelper(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;
        }

        public async Task<FileDocument> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Nenhum arquivo encontrado.");

            var date = DateTime.Now;

            var originalFileName = file.FileName;
            var extension = Path.GetExtension(originalFileName);
            var newFileName = $"{Guid.NewGuid()}{extension}";

            var uploadDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                date.Year.ToString(),
                date.Month.ToString(),
                date.Day.ToString());

            Directory.CreateDirectory(uploadDir);
            var filePath = Path.Combine(uploadDir, newFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var fileUrl = $"/files/{date.Year.ToString()}/{date.Month.ToString()}/{date.Day.ToString()}/{newFileName}";

            return new FileDocument
            {
                Id = Guid.NewGuid(),
                Name = file.FileName,
                Url = fileUrl,
                ContentType = file.ContentType,
                Size = file.Length,
                Active = true,
                CreatedAt = date
            };
        }
    }
}