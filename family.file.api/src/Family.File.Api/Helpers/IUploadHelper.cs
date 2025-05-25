using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Infrastructure.Entities;

namespace Family.File.Api.Helpers
{
    public interface IUploadHelper
    {
        Task<FileDocument> UploadFileAsync(IFormFile file,string urlBase);
        Task<FileDocument> UploadBase64FileAsync(string base64String, string originalFileName, string contentType, string urlBase);
        Task DeleteFileFromDiskAsync(FileDocument document);
    }
}