using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.DTOs;

[ApiController]
[Route("api/[controller]")]
public class CohortsController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public CohortsController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cohort>>> GetCohorts()
    {
        return await _context.Cohorts
            .Include(c => c.Programme)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cohort>> GetCohort(int id)
    {
        var cohort = await _context.Cohorts
            .Include(c => c.Programme)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cohort == null)
        {
            return NotFound();
        }

        return cohort;
    }

    [HttpPost]
    public async Task<ActionResult<Cohort>> CreateCohort(CohortCreateDto dto)
    {
        var programmeExists = await _context.Programmes
            .AnyAsync(p => p.Id == dto.ProgrammeId);

        if (!programmeExists)
        {
            return BadRequest("The Programme does not exist.");
        }

        if (dto.EndDate <= dto.StartDate)
        {
            return BadRequest("End date must be after start date.");
        }

        var cohort = new Cohort
        {
            ProgrammeId = dto.ProgrammeId,
            Name = dto.Name,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status
        };

    _context.Cohorts.Add(cohort);
    await _context.SaveChangesAsync();

    return CreatedAtAction(
        nameof(GetCohort),
        new { id = cohort.Id },
        cohort);
}
}
