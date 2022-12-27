namespace Cuddler.Web.Modules;

public interface IMenuItem
{
    List<IMenuItem>? Children { get; set; }

    int? Count { get; set; }

    bool Disabled { get; set; }

    bool Hide { get; set; }

    string? Icon { get; set; }

    ELinkType LinkType { get; set; }

    bool Locked { get; set; }

    string? PageTemplateType { get; set; }

    string? Query { get; set; }

    string Segment { get; }

    int SortOrder { get; set; }

    string? Text { get; set; }

    string? Description { get; set; }
}