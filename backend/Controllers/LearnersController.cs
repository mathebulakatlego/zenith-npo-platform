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
public async Task<ActionResult<IEnumerable<LearnerResponseDto>>> GetLearners()
{
    var learners = await _context.Learners
        .Select(l => new LearnerResponseDto
        {
            Id = l.Id,
            ApplicationId = l.ApplicationId,
            EnrolledAt = l.EnrolledAt,
            Status = l.Status
        })
        .ToListAsync();

    return Ok(learners);
}

    [HttpGet("{id}")]
    public async Task<ActionResult<LearnerResponseDto>> GetLearner(int id)
    {
        var learner = await _context.Learners
            .Where(l => l.Id == id)
            .Select(l => new LearnerResponseDto
            {
                Id = l.Id,
                ApplicationId = l.ApplicationId,
                EnrolledAt = l.EnrolledAt,
                Status = l.Status
            })
            .FirstOrDefaultAsync();

        if (learner == null)
        {
           return NotFound(new ProblemDetails
            {
                Title = "Applicant not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(learner);
    }

        
    [HttpPost]
    public async Task<ActionResult<Learner>> CreateLearner(LearnerCreateDto dto)
    {
        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.Id == dto.ApplicationId);

        if (application == null)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Application does not exist",
                Status = StatusCodes.Status400BadRequest
            });
        }

        if (application.Status != "Approved")
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Only approved applications can be converted to learners.",
                Status = StatusCodes.Status400BadRequest
            });
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
            return NotFound(new ProblemDetails
            {
                Title = "Learner not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        var eligible = await _eligibilityService.IsEligibleAsync(id);

        return Ok(eligible);
    }
}