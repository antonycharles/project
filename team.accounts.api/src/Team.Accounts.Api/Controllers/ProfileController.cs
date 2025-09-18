using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Api.Helpers;
using Team.Accounts.Core.Exceptions;
using Team.Accounts.Core.Handlers;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Team.Accounts.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProfileController : ControllerBase
    {
        
        private readonly ILogger<ProfileController> _logger;
        private readonly IProfileHandler _profileHandler;
        
        public ProfileController(
            IProfileHandler profileHandler,
            ILogger<ProfileController> logger
        )
        {
            _logger = logger;
            _profileHandler = profileHandler;
        }

        [HttpGet]
        [AuthorizeRole(RoleConstants.ProfileRole.List)]
        [ProducesResponseType(typeof(PaginatedResponse<ProfileResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginatedProfileRequest request)
        {
            try
            {
                var result = await _profileHandler.GetAsync(request);

                return Ok(result);  
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            } 
        }

        [HttpGet("{id}")]
        [AuthorizeRole(RoleConstants.ProfileRole.List)]
        [ProducesResponseType(typeof(ProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var profile = await _profileHandler.GetByIdAsync(id);

                return Ok(profile);
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
        [AuthorizeRole(RoleConstants.ProfileRole.Create)]
        [ProducesResponseType(typeof(ProfileResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] ProfileRequest request)
        {
            try
            {
                var profile = await _profileHandler.CreateAsync(request);

                return Created("", profile);
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
        [AuthorizeRole(RoleConstants.ProfileRole.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProfileRequest request)
        {
            try
            {
                await _profileHandler.UpdateAsync(id, request);

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

        [HttpPut("Permissions/{id}")]
        [AuthorizeRole(RoleConstants.ProfileRole.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePermissionsAsync(Guid id, [FromBody] Guid[] permissionsIds)
        {
            try
            {
                await _profileHandler.UpdatePermissionsAsync(id, permissionsIds);

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
        [AuthorizeRole(RoleConstants.ProfileRole.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id){
            try
            {
                await _profileHandler.DeleteAsync(id);

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