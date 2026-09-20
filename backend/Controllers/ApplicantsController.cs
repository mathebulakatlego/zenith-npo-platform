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
    public async Task<ActionResult<IEnumerable<ApplicantResponseDto>>> GetApplicants()
    {
        return await _context.Applicants
            .Select(applicant => new ApplicantResponseDto
            {
                Id = applicant.Id,
                FirstName = applicant.FirstName,
                LastName = applicant.LastName,
                Email = applicant.Email,
                PhoneNumber = applicant.PhoneNumber,
                DateOfBirth = applicant.DateOfBirth,
                Address = applicant.Address
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicantResponseDto>> GetApplicant(int id)
    {
        var applicant = await _context.Applicants
            .Where(candidate => candidate.Id == id)
            .Select(candidate => new ApplicantResponseDto
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                DateOfBirth = candidate.DateOfBirth,
                Address = candidate.Address
            })
            .FirstOrDefaultAsync();

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
            PhoneNumber = dto.PhoneNumber,
            IdNumber = dto.IdNumber,
            DateOfBirth = dto.DateOfBirth!.Value,
            Address = dto.Address
        };

        _context.Applicants.Add(applicant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetApplicant),
            new { id = applicant.Id },
            applicant);
    }
}