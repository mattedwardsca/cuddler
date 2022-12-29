namespace Cuddler.Modules;

public class DividerLink : IMenuItem
{
    public DividerLink()
    {
        LinkType = ELinkType.Divider;
    }

    public DividerLink(string text)
    {
        Text = text;
        LinkType = ELinkType.Divider;
    }

    public DividerLink(ELinkType linkType)
    {
        LinkType = linkType;
    }

    public string Id { get; set; } = string.Empty;

    public bool Required { get; set; }

    public string? PageTitle { get; set; }

    public List<IMenuItem>? Children { get; set; }

    public int? Count { get; set; }

    public bool Disabled { get; set; }

    public bool Hide { get; set; }

    public string? Icon { get; set; }

    public ELinkType LinkType { get; set; }

    public bool Locked { get; set; }

    public string? PageTemplateType { get; set; }

    public string? Query { get; set; }

    public string Segment => string.Empty;

    public int SortOrder { get; set; }

    public string? Text { get; set; }

    public string? Description { get; set; }
}