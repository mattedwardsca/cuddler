using System.Globalization;

namespace Cuddler.Core.Utils;

public static class LinkUtil
{
    public static string ToMenuLink(string str)
    {
        return Replace(str);
    }

    private static string Replace(string str)
    {
        var myTI = new CultureInfo("en-US", false).TextInfo;
        str = myTI.ToTitleCase(str);

        return str.Replace(" ", string.Empty)
                  .Replace("-", string.Empty)
                  .Replace("?", string.Empty);
    }
}