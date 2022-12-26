namespace Cuddler.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class GridTemplateAttribute : Attribute
{
    public GridTemplateAttribute(string gridTemplate)
    {
        GridTemplate = gridTemplate;
    }

    public string GridTemplate { get; }
}