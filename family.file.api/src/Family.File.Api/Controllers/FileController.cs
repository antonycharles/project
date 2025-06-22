using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.File.Infrastructure.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Family.File.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileDocumentRepository _fileDocumentRepository;

        public FileController(
            ILogger<UploadController> logger,
            IFileDocumentRepository fileDocumentRepository)
        {
            _fileDocumentRepository = fileDocumentRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewFileAsync(Guid id)
        {

            try
            {
                var fileDocument = await _fileDocumentRepository.GetByIdAsync(id);

                if (fileDocument == null)
                    return NotFound("File not found.");

                if (!System.IO.File.Exists(fileDocument.Path))
                {
                    return NotFound("File not found.");
                }

                var fileBytes = System.IO.File.ReadAllBytes(fileDocument.Path);
                return File(fileBytes, fileDocument.ContentType);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error downloading file: {ex.Message}");
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            try
            {
                var fileDocument = await _fileDocumentRepository.GetByIdAsync(id);

                if (fileDocument == null)
                    return NotFound("File not found.");

                if (!System.IO.File.Exists(fileDocument.Path))
                {
                    return NotFound("File not found.");
                }

                var fileBytes = System.IO.File.ReadAllBytes(fileDocument.Path);
                return File(fileBytes, fileDocument.ContentType, fileDocument.Name);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error downloading file: {ex.Message}");
            }
        }

    }
}