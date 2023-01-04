namespace Cuddler.Forms.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class DropdownOptionAttribute : Attribute
{
    public DropdownOptionAttribute(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }

    public string Value { get; }
}