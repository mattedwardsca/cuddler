namespace Cuddler.Forms.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class PropertyCategoryAttribute : Attribute
{
    public PropertyCategoryAttribute(string category)
    {
        Category = category;

    }

    public string Category { get; }
}