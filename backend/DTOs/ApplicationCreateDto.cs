using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ApplicationCreateDto
{
    [Required]
    public int ApplicantId { get; set; }

    [Required]
    public int ProgrammeId { get; set; }

    [Required]
    [StringLength(2000)]
    public string Motivation { get; set; } = string.Empty;
}
