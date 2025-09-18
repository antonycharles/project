using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Api.Helpers;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Exceptions;
using Team.Accounts.Core.Handlers;
using Team.Accounts.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Team.Accounts.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly IUserProfileHandler _userProfileHandler;

        public UserProfileController(
            ILogger<UserProfileController> logger, 
            IUserProfileHandler userProfileHandler)
        {
            _logger = logger;
            _userProfileHandler = userProfileHandler;
        }

        [HttpPost]
        [AuthorizeRole(RoleConstants.UserProfileRole.Create)]
        [ProducesResponseType(typeof(UserProfile), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] UserProfileRequest request)
        {
            try
            {
                var userProfile = await _userProfileHandler.CreateAsync(request);

                return Created("", userProfile);
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
        [AuthorizeRole(RoleConstants.UserProfileRole.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id){
            try
            {
                await _userProfileHandler.DeleteAsync(id);

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