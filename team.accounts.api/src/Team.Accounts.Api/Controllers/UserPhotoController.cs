using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Api.Helpers;
using Team.Accounts.Core.Exceptions;
using Team.Accounts.Core.Handlers;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Team.Accounts.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserPhotoController : ControllerBase
    {
        private readonly IUserPhotoHandler _userPhotoHandler;
        
        public UserPhotoController(IUserPhotoHandler userPhotoHandler)
        {
            _userPhotoHandler = userPhotoHandler;
        }
        
        [HttpGet("{id}")]
        [AuthorizeRole(RoleConstants.UserRole.List)]
        [ProducesResponseType(typeof(UserPhotoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _userPhotoHandler.GetByIdAsync(id);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
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
        [AuthorizeRole(RoleConstants.UserRole.Create, RoleConstants.UserRole.Update)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrCreateAsync([FromBody] UserPhotoRequest request)
        {
            try
            {
                if (request == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _userPhotoHandler.UpdateOrCreateAsync(request);
                return NoContent();
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
    }
}