using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.DTOs;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public ApplicationsController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationResponseDto>>> GetApplications()
    {
        var applications = await _context.Applications
            .Select(a => new ApplicationResponseDto
            {
                Id = a.Id,
                ApplicantId = a.ApplicantId,
                ProgrammeId = a.ProgrammeId,
                SubmittedAt = a.SubmittedAt,
                Status = a.Status
            })
            .ToListAsync();

        return Ok(applications);
    }

    [HttpGet("{id}")]
public async Task<ActionResult<ApplicationResponseDto>> GetApplication(int id)
    {
        var application = await _context.Applications
            .Where(a => a.Id == id)
            .Select(a => new ApplicationResponseDto
            {
                Id = a.Id,
                ApplicantId = a.ApplicantId,
                ProgrammeId = a.ProgrammeId,
                SubmittedAt = a.SubmittedAt,
                Status = a.Status
            })
            .FirstOrDefaultAsync();

        if (application == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Applicant not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(application);
    }

    [HttpPost]
    public async Task<ActionResult<Application>> CreateApplication(ApplicationCreateDto dto)
    {
        var applicantExists = await _context.Applicants
        .AnyAsync(a => a.Id == dto.ApplicantId);

        if (!applicantExists)
        {
            return BadRequest("The Applicant does not exist.");
        }

        var programmeExists = await _context.Programmes
            .AnyAsync(p => p.Id == dto.ProgrammeId);

        if (!programmeExists)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Application must be approved before...",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var application = new Application
        {
            ApplicantId = dto.ApplicantId,
            ProgrammeId = dto.ProgrammeId
        };

        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetApplication),
            new { id = application.Id },
            application);
}

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
         var application = await _context.Applications.FindAsync(id);

        if (application == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Applicant not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        var validStatuses = new[] { "Pending", "Approved", "Rejected" };

        if (!validStatuses.Contains(status))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Application must be approved before...",
                Status = StatusCodes.Status400BadRequest
            });
        }

        application.Status = status;

        await _context.SaveChangesAsync();

        return Ok(application);
    }
}
