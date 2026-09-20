namespace Zenith.Api.Models;

public class Application
{
    public int Id { get; set; }

    public int ApplicantId { get; set; }

    public Applicant Applicant { get; set; } = null!;

    public int ProgrammeId { get; set; }

    public Programme Programme { get; set; } = null!;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Pending";

    public string Motivation { get; set; } = string.Empty;

    public ICollection<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
}