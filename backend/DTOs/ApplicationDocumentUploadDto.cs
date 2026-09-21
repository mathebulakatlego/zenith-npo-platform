using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Zenith.Api.DTOs;

public class ApplicationDocumentUploadDto
{
    [Required]
    public string DocumentType { get; set; } = string.Empty;

    [Required]
    public IFormFile? File { get; set; }
}