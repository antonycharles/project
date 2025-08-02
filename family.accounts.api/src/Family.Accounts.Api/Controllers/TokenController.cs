using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Api.Helpers;
using Family.Accounts.Core.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Family.Accounts.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/.well-known")]
    [ApiVersion("1.0")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenKeyHandler _tokenKeyHandler;

        public TokenController(
            ILogger<TokenController> logger,
            ITokenKeyHandler tokenKeyHandler)
        {
            _logger = logger;
            _tokenKeyHandler = tokenKeyHandler;
        }

        [HttpGet("jwks.json")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(JsonWebKey), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPublicKeys()
        {
            try
            {
                var result = _tokenKeyHandler.GetPublicKeys();

                return Ok(new
                {
                    Keys = result
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}