using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Infrastructure.Entities;

namespace Family.File.Api.Helpers
{
    public interface IUploadHelper
    {
        Task<FileDocument> UploadFileAsync(IFormFile file);
    }
}