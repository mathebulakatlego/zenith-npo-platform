using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.Services;
using Zenith.Api.DTOs;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearnersController : ControllerBase
{
    private readonly ZenithDbContext _context;
    private readonly GraduationEligibilityService _eligibilityService;

    public LearnersController(
        ZenithDbContext context,
        GraduationEligibilityService eligibilityService)
    {
        _context = context;
        _eligibilityService = eligibilityService;
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
    public async Task<ActionResult<Learner>> CreateLearner(LearnerCreateDto dto)
    {
        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.Id == dto.ApplicationId);

        if (application == null)
        {
            return BadRequest("The Application does not exist.");
        }

        if (application.Status != "Approved")
        {
            return BadRequest("Only approved applications can be converted to learners.");
        }

        var learner = new Learner
        {
            ApplicationId = dto.ApplicationId
        };

        _context.Learners.Add(learner);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetLearner),
            new { id = learner.Id },
            learner);
    }

    [HttpGet("{id}/eligibility")]
    public async Task<ActionResult<bool>> GetEligibility(int id)
    {
        var learnerExists = await _context.Learners
            .AnyAsync(l => l.Id == id);

        if (!learnerExists)
        {
            return NotFound();
        }

        var eligible = await _eligibilityService.IsEligibleAsync(id);

        return Ok(eligible);
    }
}