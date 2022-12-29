namespace Cuddler.Web.Docs;

public class DocUserManualDto
{
    public string? ExecutiveSummary { get; set; }

    public string? GettingStarted { get; set; }

    public string Id { get; set; } = null!;

    public List<DocUserManualSectionDto>? Modules { get; set; } = new();

    public string? Name { get; set; }

    public string? Purpose { get; set; }

    public string Token { get; set; } = null!;
}