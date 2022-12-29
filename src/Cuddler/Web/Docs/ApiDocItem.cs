namespace Cuddler.Web.Docs;

public class ApiDocItem
{
    public string? AssemblyName { get; set; }

    public string? Description { get; set; }

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public Type Type { get; set; } = null!;

    public string? ApiUrl { get; set; }

    public bool MustBeAdminToAccessApi { get; set; }

    public bool MustBeLoggedIn { get; set; }

    // public string? GetName()
    // {
    //     return Name?.Replace("component-", "", StringComparison.InvariantCultureIgnoreCase)
    //                .Replace("block-", "", StringComparison.InvariantCultureIgnoreCase)
    //                .Replace("field-", "", StringComparison.InvariantCultureIgnoreCase)
    //                .Replace("-", " ")
    //                .ToUpperInvariant();
    // }
}