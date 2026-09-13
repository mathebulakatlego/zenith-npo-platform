using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ApplicationCreateDto
{
    [Required]
    public int ApplicantId { get; set; }

    [Required]
    public int ProgrammeId { get; set; }
}
