using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearnersController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public LearnersController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Learner>>> GetLearners()
    {
        return await _context.Learners
            .Include(l => l.Application)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Learner>> GetLearner(int id)
    {
        var learner = await _context.Learners
            .Include(l => l.Application)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (learner == null)
        {
            return NotFound();
        }

        return learner;
    }

    [HttpPost]
    public async Task<ActionResult<Learner>> CreateLearner(Learner learner)
    {
        var applicationExists = await _context.Applications
            .AnyAsync(a => a.Id == learner.ApplicationId);

        if (!applicationExists)
        {
            return BadRequest("The Application does not exist.");
        }

        _context.Learners.Add(learner);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetLearner),
            new { id = learner.Id },
            learner);
    }
}