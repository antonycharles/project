using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;

namespace Accounts.Management.Infrastructure.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<FileDocumentResponse> UploadAsync(IFormFile file);
    }
}