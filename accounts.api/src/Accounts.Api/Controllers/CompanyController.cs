using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Api.Helpers;
using Accounts.Core.Exceptions;
using Accounts.Core.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyHandler _companyHandler;

        public CompanyController(ILogger<CompanyController> logger, ICompanyHandler companyHandler)
        {
            _logger = logger;
            _companyHandler = companyHandler;
        }

        [HttpGet("{userId}/user")]
        [AuthorizeRole(RoleConstants.CompanyRole.List)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserIdAsync(Guid userId)
        {
            try
            {
                var result = await _companyHandler.GetByUserIdAsync(userId);
                return Ok(result);
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