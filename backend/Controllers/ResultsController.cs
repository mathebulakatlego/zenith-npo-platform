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
            return NotFound(new ProblemDetails
            {
                Title = "Applicant not found",
                Status = StatusCodes.Status404NotFound
            });
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
            return BadRequest(new ProblemDetails
            {
                Title = "The Learner does not exist.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var moduleExists = await _context.Modules
            .AnyAsync(m => m.Id == dto.ModuleId);

        if (!moduleExists)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "The Module does not exist.",
                Status = StatusCodes.Status400BadRequest
            });
            
        }

        var resultExists = await _context.Results
            .AnyAsync(r => r.LearnerId == dto.LearnerId && r.ModuleId == dto.ModuleId);

        if (resultExists)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "A result already exists for this Learner and Module.",
                Status = StatusCodes.Status400BadRequest
            });
            
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