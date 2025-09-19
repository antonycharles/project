using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Api.Helpers;
using File.Infrastructure.Entities;
using File.Infrastructure.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace File.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {

        private readonly IUploadHelper _uploadHelper;
        private readonly IFileDocumentRepository _fileDocumentRepository;

        public UploadController(
            ILogger<UploadController> logger,
            IUploadHelper uploadHelper,
            IFileDocumentRepository fileDocumentRepository)
        {
            _uploadHelper = uploadHelper;
            _fileDocumentRepository = fileDocumentRepository;
        }

        [HttpPost]
        public async Task<ActionResult> UploadFileAsync(IFormFile file)
        {
            FileDocument document = new FileDocument();
            try
            {
                var appId = User.GetId();
                if (file == null || file.Length == 0)
                    throw new Exception("Nenhum arquivo enviado.");

                document = await _uploadHelper.UploadFileAsync(file, appId.ToString(), this.GetUrlBase());
                document.AppId = appId;
                await _fileDocumentRepository.AddAsync(document);

                return Ok(document);
            }
            catch (Exception ex)
            {
                await RemoveFile(document);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Base64")]
        public async Task<ActionResult> UploadFileBase64Async(string fileName, [FromBody] string base64, string contentType)
        {
            FileDocument document = new FileDocument();
            try
            {
                var appId = User.GetId();
                document = await _uploadHelper.UploadBase64FileAsync(base64, fileName, contentType, appId.ToString(), this.GetUrlBase());
                document.AppId = appId;
                await _fileDocumentRepository.AddAsync(document);

                return Ok(document);
            }
            catch (Exception ex)
            {
                await RemoveFile(document);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFileAsync(Guid id)
        {
            try
            {
                var document = await _fileDocumentRepository.GetByIdAsync(id);

                if (document == null)
                    throw new Exception("Document not found");

                await _fileDocumentRepository.DeleteAsync(id);

                await _uploadHelper.DeleteFileFromDiskAsync(document);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private async Task RemoveFile(FileDocument document)
        {
            try
            {
                if (document != null && document.Url != null)
                    await _uploadHelper.DeleteFileFromDiskAsync(document);
            }
            catch (Exception)
            {
            }
        }
        
        private string GetUrlBase()
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Request.Scheme,
                Host = Request.Host.Host,
                Port = Request.Host.Port ?? (Request.Scheme == "https" ? 443 : 80)
            };

            return uriBuilder.Uri.ToString();
        }
    }
}