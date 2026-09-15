namespace Zenith.Api.DTOs;

public class LearnerResponseDto
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public string Status { get; set; } = string.Empty;
}