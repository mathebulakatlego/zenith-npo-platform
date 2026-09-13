using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ProgressCreateDto
{
    [Required]
    public int LearnerId { get; set; }

    [Required]
    public int ModuleId { get; set; }

    [Range(0, 100)]
    public int Percentage { get; set; }
}