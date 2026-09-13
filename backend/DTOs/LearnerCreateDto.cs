using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class LearnerCreateDto
{
    [Required]
    public int ApplicationId { get; set; }
}