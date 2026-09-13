using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class CohortCreateDto
{
    [Required]
    public int ProgrammeId { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public string Status { get; set; } = "Planned";
}