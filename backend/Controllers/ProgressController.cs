using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public ProgressController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Progress>>> GetProgress()
    {
        return await _context.Progress
            .Include(p => p.Learner)
            .Include(p => p.Module)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Progress>> GetProgress(int id)
    {
        var progress = await _context.Progress
            .Include(p => p.Learner)
            .Include(p => p.Module)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (progress == null)
        {
            return NotFound();
        }

        return progress;
    }

    [HttpPost]
    public async Task<ActionResult<Progress>> CreateProgress(Progress progress)
    {
        var learnerExists = await _context.Learners
            .AnyAsync(l => l.Id == progress.LearnerId);

        var moduleExists = await _context.Modules
            .AnyAsync(m => m.Id == progress.ModuleId);

        if (!learnerExists || !moduleExists)
        {
            return BadRequest("The Learner or Module does not exist.");
        }

        if (progress.Percentage < 0 || progress.Percentage > 100)
        {
            return BadRequest("Percentage must be between 0 and 100.");
        }

        _context.Progress.Add(progress);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetProgress),
            new { id = progress.Id },
            progress);
    }
}