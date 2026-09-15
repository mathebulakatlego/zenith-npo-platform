using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ProgrammeCreateDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}