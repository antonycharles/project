using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Infrastructure.Entities;

namespace File.Api.Helpers
{
    public interface IUploadHelper
    {
        Task<FileDocument> UploadFileAsync(IFormFile file, string appId, string urlBase);
        Task<FileDocument> UploadBase64FileAsync(string base64String, string originalFileName, string contentType, string appId, string urlBase);
        Task DeleteFileFromDiskAsync(FileDocument document);
    }
}