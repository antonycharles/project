using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;
using Refit;

namespace Accounts.Management.Infrastructure.Refits
{
    public interface IFileRefit
    {
        [Multipart]
        [Post("/Upload?isPublic={isPublic}")]
        Task<FileDocumentResponse> UploadAsync(StreamPart file, bool isPublic);
    }
}