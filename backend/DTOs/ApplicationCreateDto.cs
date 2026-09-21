using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ApplicationCreateDto : IValidatableObject
{
    [Required]
    public int ApplicantId { get; set; }

    [Required]
    public int ProgrammeId { get; set; }

    [Required]
    [StringLength(2000)]
    public string Motivation { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (Motivation.Trim().Length < 150)
        {
            yield return new ValidationResult(
                "Motivation must contain at least 150 meaningful characters.",
                [nameof(Motivation)]);
        }
    }
}
