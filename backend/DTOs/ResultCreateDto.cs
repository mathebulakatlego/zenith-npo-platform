using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ResultCreateDto
{
    [Required]
    public int LearnerId { get; set; }

    [Required]
    public int ModuleId { get; set; }

    [Range(0, 100)]
    public decimal Mark { get; set; }
}