using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ApplicantCreateDto
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(13, MinimumLength = 13)]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "IdNumber must contain exactly 13 digits.")]
    public string IdNumber { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [StringLength(500)]
    public string Address { get; set; } = string.Empty;
}