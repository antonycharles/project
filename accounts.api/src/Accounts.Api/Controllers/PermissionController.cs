using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Api.Helpers;
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
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionHandler _permissionHandler;
        
        public PermissionController(
            IPermissionHandler permissionHandler,
            ILogger<PermissionController> logger
        )
        {
            _logger = logger;
            _permissionHandler = permissionHandler;
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.PermissionRole.List)]
        [ProducesResponseType(typeof(PaginatedResponse<PermissionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginatedPermissionRequest request)
        {
            try
            {
                var result = await _permissionHandler.GetAsync(request);

                return Ok(result);  
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            } 
        }

        [HttpGet("{id}")]
        [AuthorizeRole(RoleConstants.PermissionRole.List)]
        [ProducesResponseType(typeof(PermissionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var permission = await _permissionHandler.GetByIdAsync(id);

                return Ok(permission);
            }
            catch(NotFoundException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.PermissionRole.Create)]
        [ProducesResponseType(typeof(PermissionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] PermissionRequest request)
        {
            try
            {
                var permission = await _permissionHandler.CreateAsync(request);

                return Created("", permission);
            }
            catch(BusinessException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [AuthorizeRole(RoleConstants.PermissionRole.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PermissionRequest request)
        {
            try
            {
                await _permissionHandler.UpdateAsync(id, request);

                return Ok();
            }
            catch(NotFoundException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch(BusinessException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpDelete("{id}")]
        [AuthorizeRole(RoleConstants.PermissionRole.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id){
            try
            {
                await _permissionHandler.DeleteAsync(id);

                return Ok();
            }
            catch(NotFoundException ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status404NotFound);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}