using Cuddler.Web.Forms;

namespace Cuddler.Core.Services.Docs.Models;

public class UxDocItem
{
    public string? AppName { get; set; }

    public string Description { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string Image { get; set; } = null!;

    public bool IsObsolete { get; set; }

    public string? Name { get; set; }

    public IEnumerable<FormField>? Properties { get; set; }

    public string TemplateName { get; set; } = null!;

    public Type Type { get; set; } = null!;

    public string? GetName()
    {
        return Name?.Replace("component-", "", StringComparison.InvariantCultureIgnoreCase)
                   .Replace("block-", "", StringComparison.InvariantCultureIgnoreCase)
                   .Replace("field-", "", StringComparison.InvariantCultureIgnoreCase)
                   .Replace("-", " ")
                   .ToUpperInvariant();
    }
}