namespace CuddlerDev.Web.Docs;

public class DocUserManualSectionDto
{
    public string? Category { get; set; }

    public string? Description { get; set; }

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public List<DocUserManualSectionPage>? Stories { get; set; } = new();

    public string? Token { get; set; }
}