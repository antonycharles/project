using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Api.Helpers;
using Family.File.Infrastructure.Entities;
using Family.File.Infrastructure.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Family.File.Api.Controllers
{
    [ApiController]
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
                if (file == null || file.Length == 0)
                    throw new Exception("Nenhum arquivo enviado.");

                document = await _uploadHelper.UploadFileAsync(file, this.GetUrlBase());
                await _fileDocumentRepository.AddAsync(document);

                return Ok(document);
            }
            catch (Exception ex)
            {
                await RemoveFile(document);
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

        [HttpPost("Base64")]
        public async Task<ActionResult> UploadFileBase64Async(string fileName, [FromBody] string base64, string contentType)
        {
            FileDocument document = new FileDocument();
            try
            {
                var fileUpload = await _uploadHelper.UploadBase64FileAsync(base64, fileName, contentType, this.GetUrlBase());
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
            await _fileDocumentRepository.DeleteAsync(id);
            return Ok();
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