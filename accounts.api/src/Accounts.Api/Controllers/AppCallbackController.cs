using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Exceptions;
using Accounts.Core.Handlers;
using Accounts.Core.Requests;
using Accounts.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AppCallbackController : ControllerBase
    {
        private readonly IAppCallbackHandler _handler;

        public AppCallbackController(IAppCallbackHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("app/{appId}")]
        [ProducesResponseType(typeof(IEnumerable<AppCallbackResponse>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllByAppIdAsync(Guid appId)
        {
            try
            {
                var result = await _handler.GetAllByAppIdAsync(appId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppCallbackResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _handler.GetByIdAsync(id);
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AppCallbackResponse), 201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] AppCallbackRequest request)
        {
            try
            {
                var result = await _handler.CreateAsync(request);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AppCallbackRequest request)
        {
            try
            {
                await _handler.UpdateAsync(id, request);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _handler.DeleteAsync(id);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}