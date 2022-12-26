namespace Cuddler.Core.Attributes;

public class IWantToAttribute : Attribute
{
    public IWantToAttribute(string iWantTo)
    {
        IWantTo = iWantTo;
    }

    public string IWantTo { get; set; }
}