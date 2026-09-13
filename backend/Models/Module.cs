namespace Zenith.Api.Models;

public class Module
{
    public int Id { get; set; }

    public int ProgrammeId { get; set; }

    public Programme Programme { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public int Order { get; set; }

    public bool IsActive { get; set; } = true;
}
