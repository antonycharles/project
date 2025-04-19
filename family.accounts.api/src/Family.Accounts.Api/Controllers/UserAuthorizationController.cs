using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Api.Helpers;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Family.Accounts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthorizationController : ControllerBase
    {
        private readonly ILogger<UserAuthorizationController> _logger;
        private readonly IUserAuthorizationHandler _userAuthenticationHandler;
        private readonly IUserHandler _userHandler;

        public UserAuthorizationController(
            ILogger<UserAuthorizationController> logger,
            IUserAuthorizationHandler userAuthenticationHandler,
            IUserHandler userHandler
        )
        {
            _logger = logger;
            _userAuthenticationHandler = userAuthenticationHandler;
            _userHandler = userHandler;
        }


        [HttpPost]
        [AuthorizeRole(RoleConstants.UserAuthorizationRole.Authorization)]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticationAsync([FromBody] UserAuthenticationRequest request)
        {
            try
            {
                var result = await _userAuthenticationHandler.AuthenticationAsync(request);

                return Ok(result);
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

        [HttpGet("userInfo/{userId}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AuthorizeRole(RoleConstants.UserAuthorizationRole.Authorization)]
        public async Task<IActionResult> UserInfoAsync(Guid userId)
        {
            try
            {
                var user = await _userHandler.GetByIdAsync(userId);
                return Ok(user);
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