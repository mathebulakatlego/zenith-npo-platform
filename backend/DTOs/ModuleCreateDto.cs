using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ModuleCreateDto
{
    [Required]
    public int ProgrammeId { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Order { get; set; }

    public bool IsActive { get; set; } = true;
}