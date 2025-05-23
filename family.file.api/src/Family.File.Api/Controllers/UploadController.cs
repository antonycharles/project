using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Family.File.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {

        private readonly IUploadHelper _uploadHelper;

        public UploadController(
            ILogger<UploadController> logger,
            IUploadHelper uploadHelper)
        {
            _uploadHelper = uploadHelper;
        }

        [HttpPost]
        public async Task<ActionResult> UploadFileAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new Exception("Nenhum arquivo enviado.");
                    
                    var document = await _uploadHelper.UploadFileAsync(file);
                    return Ok(document);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Base64")]
        public async Task<ActionResult> UploadFileBase64Async(string fileName, [FromBody] string base64)
        {
            //var fileUpload = await _fileUploadHandler.UploadFileBase64Async(base64, fileName);
            return Ok();
            //return Ok(fileUpload);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFileAsync(Guid id)
        {

            //await _fileUploadHandler.DeleteFileAsync(hash);

            return Ok();

        }
    }
}