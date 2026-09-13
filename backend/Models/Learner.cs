namespace Zenith.Api.Models;

public class Learner
{
    public int Id { get; set; }

    public int ApplicationId { get; set; }

    public Application Application { get; set; } = null!;

    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Active";
}