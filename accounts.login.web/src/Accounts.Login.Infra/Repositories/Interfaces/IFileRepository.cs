using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Login.Infra.Requests;
using Accounts.Login.Infra.Responses;
using Microsoft.AspNetCore.Http;

namespace Accounts.Login.Infra.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<FileDocumentResponse> UploadAsync(IFormFile file);
    }
}