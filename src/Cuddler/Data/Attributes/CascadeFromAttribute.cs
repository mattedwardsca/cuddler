namespace Cuddler.Data.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class CascadeFromAttribute : Attribute
{
    public CascadeFromAttribute(string cascadeFrom)
    {
        CascadeFrom = cascadeFrom;
    }

    public string CascadeFrom { get; }
}