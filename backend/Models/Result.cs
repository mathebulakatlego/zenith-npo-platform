namespace Zenith.Api.Models;

public class Result
{
    public int Id { get; set; }

    public int LearnerId { get; set; }

    public Learner Learner { get; set; } = null!;

    public int ModuleId { get; set; }

    public Module Module { get; set; } = null!;

    public decimal Mark { get; set; }
}
