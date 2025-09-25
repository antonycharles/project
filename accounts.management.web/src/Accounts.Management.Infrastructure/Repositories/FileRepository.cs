using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Management.Infrastructure.Exceptions;
using Accounts.Management.Infrastructure.Refits;
using Accounts.Management.Infrastructure.Repositories.Interfaces;
using Accounts.Management.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;
using Refit;

namespace Accounts.Management.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IFileRefit _fileRefit;

        public FileRepository(IFileRefit appRefit)
        {
            _fileRefit = appRefit;
        }

        public async Task<FileDocumentResponse> UploadAsync(IFormFile file)
        {
            try
            {
                var streamPart = new StreamPart(file.OpenReadStream(), file.FileName, file.ContentType);
                return await _fileRefit.UploadAsync(streamPart);
            }
            catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var problemDetails = await ex.GetContentAsAsync<ProblemDetails>();

                if (problemDetails.Detail != null)
                    throw new ExternalApiException(problemDetails.Detail);

                throw new ExternalApiException(problemDetails.Errors.FirstOrDefault().Value.FirstOrDefault());
            }
            catch (ApiException ex)
            {
                throw new Exception(ex.Content.ToString());
            }
        }
    }
}