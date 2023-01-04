namespace Cuddler.Forms.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class TabindexAttribute : Attribute
{
    public TabindexAttribute(int tabindex)
    {
        Tabindex = tabindex;
    }

    public int Tabindex { get; }
}