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

        public async Task<FileDocument> UploadFileAsync(IFormFile file, string urlBase = null)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Nenhum arquivo encontrado.");

            var date = DateTime.Now;
            var id = Guid.NewGuid();

            var originalFileName = file.FileName;
            var extension = Path.GetExtension(originalFileName);
            var newFileName = $"{id}{extension}";
            string uploadDir = MontaDiretorio(date);

            Directory.CreateDirectory(uploadDir);
            var filePath = Path.Combine(uploadDir, newFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var fileUrl = $"/files/{date.Year.ToString()}/{date.Month.ToString()}/{date.Day.ToString()}/{newFileName}";

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

        private static string MontaDiretorio(DateTime date)
        {
            return Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                date.Year.ToString(),
                date.Month.ToString(),
                date.Day.ToString());
        }

        public async Task<FileDocument> UploadBase64FileAsync(string base64String, string originalFileName, string contentType, string urlBase = null)
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

            var uploadDir = MontaDiretorio(date);

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

            var fileUrl = $"/files/{date.Year}/{date.Month}/{date.Day}/{newFileName}";

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
            if (string.IsNullOrWhiteSpace(document?.Url))
                throw new Exception("Invalid file.");

            var relativePath = document.Url.Replace("/files", "").TrimStart('/');

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            await Task.CompletedTask;
        }
    }
}