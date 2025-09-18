using Microsoft.AspNetCore.Mvc;
using Team.Application.Interfaces;
using Team.Application.DTOs;
using Team.Domain.Exceptions;

namespace Team.Api.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
[ApiVersion("1.0")]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly ITeamService _teamService;

    public TeamController(ILogger<TeamController> logger, ITeamService teamService)
    {
        _logger = logger;
        _teamService = teamService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TeamDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
    {
        try
        {
            var families = await _teamService.GetAllAsync();
            return Ok(families);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching families");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TeamDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TeamDto>> GetById(Guid id)
    {
        try
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
                return NotFound("Team not found");
            return Ok(team);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching team by id");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<TeamDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetByUserId(Guid userId)
    {
        try
        {
            var families = await _teamService.GetByUserIdAsync(userId);
            return Ok(families);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching families by userId");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(TeamDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TeamDto>> Create([FromBody] CreateTeamDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _teamService.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating team");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeamDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _teamService.UpdateAsync(dto);
            
            return NoContent();
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating team");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _teamService.DeleteAsync(id);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting team");
            return StatusCode(500, "Internal server error");
        }
    }
}
