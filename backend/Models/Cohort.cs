namespace Zenith.Api.Models;

public class Cohort
{
    public int Id { get; set; }

    public int ProgrammeId { get; set; }

    public Programme Programme { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = "Planned";
}