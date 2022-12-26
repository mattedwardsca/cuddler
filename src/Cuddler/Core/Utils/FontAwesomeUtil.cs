using Cuddler.Core.Data;

namespace Cuddler.Core.Utils;

public static class FontAwesomeUtil
{
    public static string ToString(EFontAwesomeSize? size)
    {
        if (size == null)
        {
            return string.Empty;
        }

        return size switch
        {
            EFontAwesomeSize.XS => "fa-xs",
            EFontAwesomeSize.SM => "fa-sm",
            EFontAwesomeSize.LG => "fa-lg",
            EFontAwesomeSize.TwoX => "fa-2x",
            EFontAwesomeSize.ThreeX => "fa-3x",
            EFontAwesomeSize.FiveX => "fa-5x",
            EFontAwesomeSize.SevenX => "fa-7x",
            EFontAwesomeSize.TenX => "fa-10x",
            EFontAwesomeSize.Default => string.Empty,
            var _ => string.Empty
        };
    }
}