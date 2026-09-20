namespace Zenith.Api.DTOs;

public class ApplicationResponseDto
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    public int ProgrammeId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Motivation { get; set; } = string.Empty;
}