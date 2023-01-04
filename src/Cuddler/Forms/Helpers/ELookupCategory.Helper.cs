using System.Text.RegularExpressions;

namespace Cuddler.Forms.Helpers;

public static class ELookupCategoryHelper
{
    public static string[] List()
    {
        return Enum.GetNames(typeof(ELookupCategory));
    }

    public static ELookupCategory Parse(string sEnum)
    {
        return (ELookupCategory)Enum.Parse(typeof(ELookupCategory), sEnum, true);
    }

    public static string ToString(ELookupCategory eEnum)
    {
        return ToSplitCamelCase(Enum.GetName(typeof(ELookupCategory), eEnum) ?? string.Empty);
    }


    private static string ToSplitCamelCase(string str)
    {
        return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }
}