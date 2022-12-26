using System.Text.RegularExpressions;
using System.Web;

namespace Cuddler.Core.Utils;

public static class StringUtil
{
    public static string? CleanName(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        text = RemoveHtml(text);
        text = text == null
            ? null
            : Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text);

        text = ReplaceNumbers(text);
        text = text?.Replace(' ', '-');
        text = text?.ToLower();
        text = $"/{text}";

        return text;
    }

    public static string? CleanNamespace(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        text = RemoveHtml(text);
        text = text == null
            ? null
            : Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text);
        text = ReplaceNumbers(text);
        text = text?.Replace(" ", string.Empty);
        text = text?.Replace('-', '.');
        text = $"{text}";

        return text;
    }

    public static string? CleanResourceUri(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        text = RemoveHtml(text);
        text = text == null
            ? null
            : Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text);
        text = ReplaceNumbers(text);
        text = text?.Replace(' ', '-');
        text = text?.ToLower();
        text = $"/{text}";

        return text;
    }

    public static string? CleanTitle(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        text = RemoveHtml(text);
        text = text == null
            ? null
            : string.Concat(text.Select(x => char.IsUpper(x)
                        ? " " + x
                        : x.ToString()))
                    .TrimStart(' ');
        text = text == null
            ? null
            : Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text);
        text = ReplaceNumbers(text);
        text = text?.Replace(" ", string.Empty);

        return text;
    }

    public static string[]? CommaSplit(string? str)
    {
        return string.IsNullOrEmpty(str)
            ? null
            : str.Split(",");
    }

    public static TType Convert<TType>(string? value)
    {
        var memberInfo = typeof(TType);
        if (memberInfo == typeof(int))
        {
            return (TType)System.Convert.ChangeType(string.IsNullOrEmpty(value)
                ? 0
                : value, typeof(TType));
        }

        if (memberInfo == typeof(decimal))
        {
            return (TType)System.Convert.ChangeType(string.IsNullOrEmpty(value)
                ? 0
                : value, typeof(TType));
        }

        if (memberInfo == typeof(double))
        {
            return (TType)System.Convert.ChangeType(string.IsNullOrEmpty(value)
                ? 0
                : value, typeof(TType));
        }

        if (memberInfo == typeof(float))
        {
            return (TType)System.Convert.ChangeType(string.IsNullOrEmpty(value)
                ? 0
                : value, typeof(TType));
        }

        if (memberInfo == typeof(bool))
        {
            return (TType)System.Convert.ChangeType(string.IsNullOrEmpty(value)
                ? false
                : value, typeof(TType));
        }

        return (TType)(object)value!;
    }

    public static string? GetSubstring(string? str, int characters)
    {
        if (str == null)
        {
            return null;
        }

        str = str.Trim();
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        return str.Length <= characters
            ? str
            : str[..characters];
    }

    public static bool HasValue(string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

    public static string HasValue(string? value, string trueValue, string falseValue = "")
    {
        return !string.IsNullOrEmpty(value)
            ? trueValue
            : falseValue;
    }

    public static string? HtmlDecode(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        var myWriter = new StringWriter();
        HttpUtility.HtmlDecode(text, myWriter);

        return myWriter.ToString();
    }

    public static string? HtmlEncode(string? text)
    {
        return HttpUtility.HtmlEncode(text);
    }

    public static bool IsTrue(string? str)
    {
        return !string.IsNullOrEmpty(str) && str.Equals("true", StringComparison.InvariantCultureIgnoreCase);
    }

    public static string? Join(string[]? strArray)
    {
        return strArray == null
            ? null
            : string.Join(", ", strArray);
    }

    public static string RemoveAllWhiteSpace(string text)
    {
        return !string.IsNullOrEmpty(text)
            ? text.ToCharArray()
                  .Where(c => !char.IsWhiteSpace(c))
                  .Select(c => c.ToString())
                  .Aggregate((a, b) => a + b)
            : text;
    }

    public static string? RemoveHiddenCharacters(string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        str = str.Trim();

        string output = new(str.Where(c => !char.IsControl(c))
                               .ToArray());

        return output;
    }

    public static string? RemoveHtml(string? text)
    {
        if (text == null)
        {
            return null;
        }

        var rgx = new Regex("[^a-zA-Z0-9 -]");
        var result = rgx.Replace(text, "");

        return result;
    }

    public static string? ReplaceNumbers(string? result)
    {
        if (result == null)
        {
            return null;
        }

        result = result.Replace("1", "One");
        result = result.Replace("2", "Two");
        result = result.Replace("3", "Three");
        result = result.Replace("4", "Four");
        result = result.Replace("5", "Five");
        result = result.Replace("6", "Six");
        result = result.Replace("7", "Seven");
        result = result.Replace("8", "Eight");
        result = result.Replace("9", "Nine");
        result = result.Replace("0", "Zero");

        return result;
    }

    public static string? ReturnNullIfEmpty(string? str)
    {
        if (str == null)
        {
            return null;
        }

        str = str.Trim();

        return str == ""
            ? null
            : str;
    }

    public static bool SameValue(string? obj1, string? obj2)
    {
        if (string.IsNullOrEmpty(obj1) && string.IsNullOrEmpty(obj2))
        {
            return true;
        }

        return Equals(obj1, obj2);
    }

    public static string SplitCamelCase(string str)
    {
        return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }

    public static string ToDate(DateTime date)
    {
        return date.ToString("MMM d, yyyy");
    }

    public static string ToPascalCase(string title)
    {
        var invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
        var whiteSpace = new Regex(@"(?<=\s)");
        var startsWithLowerCaseChar = new Regex("^[a-z]");
        var firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
        var lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
        var upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

        // replace white spaces with undescore, then replace all invalid chars with empty string
        var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(title, "_"), string.Empty)
                                        // split by underscores
                                        .Split(new[]
                                        {
                                            '_'
                                        }, StringSplitOptions.RemoveEmptyEntries)
                                        // set first letter to uppercase
                                        .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                                        // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
                                        .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                                        // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
                                        .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                                        // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
                                        .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

        return string.Concat(pascalCase);
    }

    public static string ToReplace(string str, string characterToReplace)
    {
        return string.IsNullOrEmpty(str)
            ? str
            : str.Replace(characterToReplace, " ");
    }

    public static string ToTime(int totalMinutes)
    {
        var hours = totalMinutes / 60;
        var minutes = totalMinutes % 60;

        return $"{hours:00}:{minutes:00}";
    }

    public static string ToUppercaseFirst(string s)
    {
        return char.ToUpper(s[0]) + s[1..];
    }
}