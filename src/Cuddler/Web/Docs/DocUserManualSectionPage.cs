namespace Cuddler.Web.Docs;

public class DocUserManualSectionPage
{
    public string? Actor { get; set; }

    public string? Category { get; set; }

    public string Id { get; set; } = null!;

    public bool IsApproved { get; set; }

    public string? SoThat { get; set; }

    public string? StartUrl { get; set; }

    public string? Steps { get; set; }

    public string? Token { get; set; }

    public string? UserWant { get; set; }

    public string ToStory()
    {
        return $"As a {Actor}, I want to {UserWant}.";
    }
}