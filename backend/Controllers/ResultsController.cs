using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.DTOs;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public ResultsController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Result>>> GetResults()
    {
        return await _context.Results
            .Include(r => r.Learner)
            .Include(r => r.Module)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result>> GetResult(int id)
    {
        var result = await _context.Results
            .Include(r => r.Learner)
            .Include(r => r.Module)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<Result>> CreateResult(ResultCreateDto dto)
    {
        var learnerExists = await _context.Learners
            .AnyAsync(l => l.Id == dto.LearnerId);

        if (!learnerExists)
        {
            return BadRequest("The Learner does not exist.");
        }

        var moduleExists = await _context.Modules
            .AnyAsync(m => m.Id == dto.ModuleId);

        if (!moduleExists)
        {
            return BadRequest("The Module does not exist.");
        }

        var resultExists = await _context.Results
            .AnyAsync(r => r.LearnerId == dto.LearnerId && r.ModuleId == dto.ModuleId);

        if (resultExists)
        {
            return BadRequest("A result already exists for this Learner and Module.");
        }

        var result = new Result
        {
            LearnerId = dto.LearnerId,
            ModuleId = dto.ModuleId,
            Mark = dto.Mark
        };

        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetResult),
            new { id = result.Id },
            result);
    }
}