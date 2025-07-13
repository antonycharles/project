using Microsoft.AspNetCore.Mvc;
using Family.Application.Interfaces;
using Family.Application.DTOs;
using Family.Domain.Exceptions;

namespace Family.Api.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
[ApiVersion("1.0")]
public class FamilyController : ControllerBase
{
    private readonly ILogger<FamilyController> _logger;
    private readonly IFamilyService _familyService;

    public FamilyController(ILogger<FamilyController> logger, IFamilyService familyService)
    {
        _logger = logger;
        _familyService = familyService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FamilyDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<FamilyDto>>> GetAll()
    {
        try
        {
            var families = await _familyService.GetAllAsync();
            return Ok(families);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching families");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FamilyDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<FamilyDto>> GetById(Guid id)
    {
        try
        {
            var family = await _familyService.GetByIdAsync(id);
            if (family == null)
                return NotFound("Family not found");
            return Ok(family);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching family by id");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<FamilyDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<FamilyDto>>> GetByUserId(Guid userId)
    {
        try
        {
            var families = await _familyService.GetByUserIdAsync(userId);
            return Ok(families);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching families by userId");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(FamilyDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<FamilyDto>> Create([FromBody] CreateFamilyDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _familyService.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating family");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFamilyDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _familyService.UpdateAsync(dto);
            
            return NoContent();
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating family");
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
            await _familyService.DeleteAsync(id);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business error");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting family");
            return StatusCode(500, "Internal server error");
        }
    }
}
