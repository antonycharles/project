using Team.File.Infrastructure.Entities;
using Team.File.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Team.File.Api.Helpers
{
    public class UploadHelper : IUploadHelper
    {
        private readonly FileSettings _fileSettings;

        public UploadHelper(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;
        }

        public async Task<FileDocument> UploadFileAsync(IFormFile file, string appId, string urlBase = null)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Nenhum arquivo encontrado.");

            var date = DateTime.Now;
            var id = Guid.NewGuid();

            var originalFileName = file.FileName;
            var extension = Path.GetExtension(originalFileName);
            var newFileName = $"{id}{extension}";

            string uploadDir = GenerateDirectoryString(date, appId);

            Directory.CreateDirectory(uploadDir);
            var filePath = Path.Combine(uploadDir, newFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var fileUrl = GenerateUrlString(newFileName, date, appId);

            return new FileDocument
            {
                Id = id,
                Name = file.FileName,
                Url = new Uri(new Uri(urlBase), fileUrl).ToString(),
                Path = filePath,
                ContentType = file.ContentType,
                Size = file.Length,
                Active = true,
                CreatedAt = date
            };
        }

        public async Task<FileDocument> UploadBase64FileAsync(string base64String, string originalFileName, string contentType, string appId, string urlBase = null)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                throw new Exception("Nenhum conteúdo base64 fornecido.");

            var date = DateTime.Now;
            var id = Guid.NewGuid();

            var extension = Path.GetExtension(originalFileName);
            if (string.IsNullOrEmpty(extension))
            {
                // Tenta deduzir a extensão do contentType
                extension = contentType switch
                {
                    "image/jpeg" => ".jpg",
                    "image/png" => ".png",
                    "application/pdf" => ".pdf",
                    _ => ".bin"
                };
            }

            var newFileName = $"{id}{extension}";

            var uploadDir = GenerateDirectoryString(date, appId);

            Directory.CreateDirectory(uploadDir);

            var filePath = Path.Combine(uploadDir, newFileName);

            byte[] fileBytes;
            try
            {
                if (base64String.Contains(","))
                    base64String = base64String.Split(',')[1];

                fileBytes = Convert.FromBase64String(base64String);
            }
            catch
            {
                throw new Exception("Base64 inválido.");
            }

            await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

            var fileUrl = GenerateUrlString(newFileName, date, appId);

            return new FileDocument
            {
                Id = id,
                Name = originalFileName,
                Url = new Uri(new Uri(urlBase), fileUrl).ToString(),
                Path = filePath,
                ContentType = contentType,
                Size = fileBytes.Length,
                Active = true,
                CreatedAt = date
            };
        }

        public async Task DeleteFileFromDiskAsync(FileDocument document)
        {
            if (string.IsNullOrWhiteSpace(document?.Path))
                throw new Exception("Invalid file.");


            if (System.IO.File.Exists(document.Path))
            {
                System.IO.File.Delete(document.Path);
            }

            await Task.CompletedTask;
        }


        private static string GenerateDirectoryString(DateTime date, string appId, string subDirectory = "default")
        {
            return Path.Combine(
                "Uploads",
                appId,
                subDirectory,
                date.Year.ToString(),
                date.Month.ToString(),
                date.Day.ToString());
        }

        private static string GenerateUrlString(string newFileName, DateTime date, string appId, string subDirectory = "default")
        {
            return Path.Combine(
                "files",
                appId,
                subDirectory,
                date.Year.ToString(),
                date.Month.ToString(),
                date.Day.ToString(),
                newFileName);
        }
    }
}