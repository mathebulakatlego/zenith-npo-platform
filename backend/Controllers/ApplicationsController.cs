using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;

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
    public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
    {
        return await _context.Applications
            .Include(a => a.Applicant)
            .Include(a => a.Programme)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Application>> GetApplication(int id)
    {
        var application = await _context.Applications
            .Include(a => a.Applicant)
            .Include(a => a.Programme)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (application == null)
        {
            return NotFound();
        }

        return application;
    }

    [HttpPost]
    public async Task<ActionResult<Application>> CreateApplication(Application application)
    {
        var applicantExists = await _context.Applicants
            .AnyAsync(a => a.Id == application.ApplicantId);

        var programmeExists = await _context.Programmes
            .AnyAsync(p => p.Id == application.ProgrammeId);

        if (!applicantExists || !programmeExists)
        {
            return BadRequest("The Applicant or Programme does not exist.");
        }

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
            return NotFound();
        }

        var validStatuses = new[] { "Pending", "Approved", "Rejected" };

        if (!validStatuses.Contains(status))
        {
            return BadRequest("Status must be Pending, Approved, or Rejected.");
        }

        application.Status = status;

        await _context.SaveChangesAsync();

        return Ok(application);
    }
}
