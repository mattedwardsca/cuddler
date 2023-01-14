namespace CuddlerDev.Forms.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PlaceholderAttribute : Attribute
{
    public PlaceholderAttribute(string placeholder)
    {
        Placeholder = placeholder;
    }

    public string Placeholder { get; }
}