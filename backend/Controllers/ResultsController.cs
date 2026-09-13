using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;

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
    public async Task<ActionResult<Result>> CreateResult(Result result)
    {
        var learnerExists = await _context.Learners
            .AnyAsync(l => l.Id == result.LearnerId);

        var moduleExists = await _context.Modules
            .AnyAsync(m => m.Id == result.ModuleId);

        var resultExists = await _context.Results
            .AnyAsync(r => r.LearnerId == result.LearnerId && r.ModuleId == result.ModuleId);

        if (resultExists)
        {
            return BadRequest("A result already exists for this Learner and Module.");
        }

        if (!learnerExists || !moduleExists)
        {
            return BadRequest("The Learner or Module does not exist.");
        }

        if (result.Mark < 0 || result.Mark > 100)
        {
            return BadRequest("Mark must be between 0 and 100.");
        }

        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetResult),
            new { id = result.Id },
            result);
    }
}