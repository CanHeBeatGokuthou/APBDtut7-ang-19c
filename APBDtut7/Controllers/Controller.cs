using ApbdTutorial7.DTOs;
using ApbdTutorial7.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApbdTutorial7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PCsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _pcService.GetAllPcsAsync();
        return Ok(result);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetByIdWithComponents(int id)
    {
        var result = await _pcService.GetPcWithComponentsAsync(id);
        if (result == null) return NotFound($"PC with ID {id} not found.");
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePcDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
    
        var created = await _pcService.CreatePcAsync(dto);
    
        return CreatedAtAction(nameof(GetByIdWithComponents), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreatePcDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _pcService.UpdatePcAsync(id, dto);
        if (!updated) return NotFound($"PC with ID {id} not found.");
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeletePcAsync(id);
        if (!deleted) return NotFound($"PC with ID {id} not found.");
        return NoContent();
    }
}