using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.Models;

public class ApplicationDocument
{
    public int Id { get; set; }

    public int ApplicationId { get; set; }

    public Application Application { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string DocumentType { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string FilePath { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}