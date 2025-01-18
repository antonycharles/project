using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Family.Accounts.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController : ControllerBase
{
    private readonly ILogger<AppController> _logger;
    private readonly IAppHandler _appHandler;
    
    public AppController(
        IAppHandler appHandler,
        ILogger<AppController> logger
    )
    {
        _logger = logger;
        _appHandler = appHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<AppResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync([FromQuery]PaginatedRequest request)
    {
        try
        {
            var result = await _appHandler.GetAppsAsync(request);

            return Ok(result);  
        }
        catch(Exception ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        } 
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AppResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var app = await _appHandler.GetByIdAsync(id);

            return Ok(app);
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
    [ProducesResponseType(typeof(AppResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] AppRequest request)
    {
        try
        {
            var app = await _appHandler.CreateAsync(request);

            return Created("", app);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AppRequest request)
    {
        try
        {
            await _appHandler.UpdateAsync(id, request);

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id){
        try
        {
            await _appHandler.DeleteAsync(id);

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
