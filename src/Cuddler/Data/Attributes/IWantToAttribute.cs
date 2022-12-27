namespace Cuddler.Data.Attributes;

public class IWantToAttribute : Attribute
{
    public IWantToAttribute(string iWantTo)
    {
        IWantTo = iWantTo;
    }

    public string IWantTo { get; set; }
}