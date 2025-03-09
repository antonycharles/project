using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public UserAuthorizationController(
            ILogger<UserAuthorizationController> logger,
            IUserAuthorizationHandler userAuthenticationHandler
        )
        {
            _logger = logger;
            _userAuthenticationHandler = userAuthenticationHandler;
        }


        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticationAsync([FromBody] UserAuthenticationRequest request)
        {
            try
            {
                var user = await _userAuthenticationHandler.AuthenticationAsync(request);

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