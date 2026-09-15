using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.DTOs;

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
            return NotFound(new ProblemDetails
            {
                Title = "Progress not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return progress;
    }

    [HttpPost]
    public async Task<ActionResult<Progress>> CreateProgress(ProgressCreateDto dto)
    {
        var learnerExists = await _context.Learners
            .AnyAsync(l => l.Id == dto.LearnerId);

        if (!learnerExists)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Learner does not exist",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var moduleExists = await _context.Modules
            .AnyAsync(m => m.Id == dto.ModuleId);

        if (!moduleExists)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Module does not exist",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var progressExists = await _context.Progress
            .AnyAsync(p =>
                p.LearnerId == dto.LearnerId &&
                p.ModuleId == dto.ModuleId);

        if (progressExists)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Progress already exists for this Learner and Module.",
                Status = StatusCodes.Status400BadRequest
            });
            
        }

        var status = dto.Percentage switch
        {
            0 => "Not Started",
            100 => "Completed",
            _ => "In Progress"
        };

        var progress = new Progress
        {
            LearnerId = dto.LearnerId,
            ModuleId = dto.ModuleId,
            Percentage = dto.Percentage,
            Status = status
        };

        _context.Progress.Add(progress);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetProgress),
            new { id = progress.Id },
            progress);
    }
}