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
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientHandler _clientHandler;

        public ClientController(
            ILogger<ClientController> logger,
            IClientHandler clientHandler
        )
        {
            _logger = logger;
            _clientHandler = clientHandler;
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ClientRole.List)]
        [ProducesResponseType(typeof(PaginatedResponse<ClientResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginatedRequest request)
        {
            try
            {
                var result = await _clientHandler.GetAsync(request);

                return Ok(result);  
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            } 
        }

        [HttpGet("{id}")]
        [AuthorizeRole(RoleConstants.ClientRole.List)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var client = await _clientHandler.GetByIdAsync(id);

                return Ok(client);
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
        [AuthorizeRole(RoleConstants.ClientRole.Create)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] ClientRequest request)
        {
            try
            {
                var client = await _clientHandler.CreateAsync(request);

                return Created("", client);
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
        [AuthorizeRole(RoleConstants.ClientRole.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ClientUpdateRequest request)
        {
            try
            {
                await _clientHandler.UpdateAsync(id, request);

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
        [AuthorizeRole(RoleConstants.ClientRole.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id){
            try
            {
                await _clientHandler.DeleteAsync(id);

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