using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.DTOs;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgrammesController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public ProgrammesController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Programme>>> GetProgrammes()
    {
        return await _context.Programmes.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Programme>> GetProgramme(int id)
    {
        var programme = await _context.Programmes.FindAsync(id);

        if (programme is null)
        {
            return NotFound();
        }

        return programme;
    }

    [HttpPost]
    public async Task<ActionResult<Programme>> CreateProgramme(ProgrammeCreateDto dto)
    {
        var programme = new Programme
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.Programmes.Add(programme);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetProgramme),
            new { id = programme.Id },
            programme);
    }
}