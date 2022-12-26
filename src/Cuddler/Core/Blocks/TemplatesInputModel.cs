namespace Cuddler.Core.Blocks;

public class TemplatesInputModel
{
    public IEnumerable<TemplatesInputModel>? Children { get; set; }

    public string? Description { get; set; }

    public string? ErrorMessage { get; set; }

    public EInput Input { get; set; }

    public string? Label { get; set; }

    public string Name { get; set; } = null!;

    public string? Placeholder { get; set; }

    public bool Required { get; set; }

    public int SortOrder { get; set; }

    public string? Value { get; set; }
}