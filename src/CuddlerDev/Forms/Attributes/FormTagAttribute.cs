namespace CuddlerDev.Forms.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class FormTagAttribute : Attribute
{
    public FormTagAttribute(string tag)
    {
        Tag = tag;
    }

    public string Tag { get; }
}