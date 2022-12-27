namespace Cuddler.Web.Helpers;

public static class EFontSizeHelper
{
    public static string ToFontSize(EFontSize fontSize)
    {
        return fontSize switch
        {
            EFontSize.OneX => "badge-size-1",
            EFontSize.TwoX => "badge-size-2",
            EFontSize.ThreeX => "badge-size-3",
            EFontSize.FourX => "badge-size-4",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}