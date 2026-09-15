using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.Models;
using Zenith.Api.DTOs;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicantsController : ControllerBase
{
    private readonly ZenithDbContext _context;

    public ApplicantsController(ZenithDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
    {
        return await _context.Applicants.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Applicant>> GetApplicant(int id)
    {
        var applicant = await _context.Applicants.FindAsync(id);

        if (applicant == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Applicant not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return applicant;
    }

    [HttpPost]
    public async Task<ActionResult<Applicant>> CreateApplicant(ApplicantCreateDto dto)
    {
        var applicant = new Applicant
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };

        _context.Applicants.Add(applicant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetApplicant),
            new { id = applicant.Id },
            applicant);
    }
}