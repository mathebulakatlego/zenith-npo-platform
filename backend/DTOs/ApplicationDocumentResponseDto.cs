namespace Zenith.Api.DTOs;

public class ApplicationDocumentResponseDto
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}