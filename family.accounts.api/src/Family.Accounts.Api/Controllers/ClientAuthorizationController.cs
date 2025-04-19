using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Api.Helpers;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Family.Accounts.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClientAuthorizationController : ControllerBase
    {
        private readonly ILogger<UserAuthorizationController> _logger;
        private readonly IClientHandler _clientHandler;
        private readonly IClientAuthorizationHandler _clientAuthorizationHandler;

        public ClientAuthorizationController(
            ILogger<UserAuthorizationController> logger,
            IClientHandler clientHandler,
            IClientAuthorizationHandler clientAuthorizationHandler
        )
        {
            _logger = logger;
            _clientHandler = clientHandler;
            _clientAuthorizationHandler = clientAuthorizationHandler;
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticationAsync([FromBody] ClientAuthenticationRequest request)
        {
            try
            {
                var user = await _clientAuthorizationHandler.AuthenticationAsync(request);

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

        [HttpGet("userInfo")]
        [AuthorizeRole(RoleConstants.UserAuthorizationRole.Authorization)]
        public async Task<IActionResult> UserInfoAsync(Guid clientId)
        {
            try
            {
                var client = await _clientHandler.GetByIdAsync(clientId);
                return Ok(client);
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