namespace Cuddler.Helpers;

public enum ETagWidth
{
    Mini,
    Small,
    Medium,
    Large,
    Paper,
    Full,
    Grow,
    W200px,
    W400px,
    None
}

public static class ETagWidthHelper
{
    public static string ToCssClass(this ETagWidth? element)
    {
        return $"eux-Width-{element.ToString()}";
    }
}