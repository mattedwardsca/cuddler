using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Core.Blocks;

public static class EInputHelper
{
    public static string[] List()
    {
        return Enum.GetNames(typeof(EInput));
    }

    public static IEnumerable<SelectListItem> ListValues()
    {
        var strings = Enum.GetNames(typeof(EInput))
                          .OrderBy(o => o);
        foreach (var s in strings)
        {
            yield return new SelectListItem(SplitCamelCase(s), s);
        }
    }

    public static EInput Parse(string? sEnum)
    {
        if (sEnum == null)
        {
            return EInput.Text;
        }

        return (EInput)Enum.Parse(typeof(EInput), sEnum, true);
    }

    public static string ToString(EInput eInputEnum)
    {
        return Enum.GetName(typeof(EInput), eInputEnum) ?? string.Empty;
    }

    private static string SplitCamelCase(string str)
    {
        return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }
}