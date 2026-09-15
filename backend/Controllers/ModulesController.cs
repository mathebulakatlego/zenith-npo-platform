using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.DTOs;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public ModulesController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Module>>> GetModules()
    {
        return await _context.Modules
            .Include(m => m.Programme)
            .OrderBy(m => m.Order)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Module>> GetModule(int id)
    {
        var module = await _context.Modules
            .Include(m => m.Programme)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (module == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Module not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return module;
    }

    [HttpPost]
    public async Task<ActionResult<Module>> CreateModule(ModuleCreateDto dto)
    {
        var programmeExists = await _context.Programmes
            .AnyAsync(p => p.Id == dto.ProgrammeId);

        if (!programmeExists)
        {
           return BadRequest(new ProblemDetails
            {
                Title = "Programme does not exist",
                Status = StatusCodes.Status400BadRequest
            });
        }

        if (dto.Order < 1)
        {
            return BadRequest(new ProblemDetails
{
    Title = "Module order must be a positive integer.",
    Status = StatusCodes.Status400BadRequest
});
        }

        var module = new Module
        {
            ProgrammeId = dto.ProgrammeId,
            Name = dto.Name,
            Order = dto.Order,
            IsActive = dto.IsActive
        };

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetModule),
            new { id = module.Id },
            module);
    }
}