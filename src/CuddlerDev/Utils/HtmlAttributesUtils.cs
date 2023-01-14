using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace CuddlerDev.Utils;

public static class HtmlAttributesUtils
{
    public static void AddIfMissing(this IDictionary<string, object?> instance, string key, object? value)
    {
        if (!instance.ContainsKey(key) && value != null)
        {
            instance[key] = value;
        }
    }

    public static void AppendClass(this IDictionary<string, object?> dictionary, string parentStyles)
    {
        dictionary.AppendValue("class", " ", parentStyles);
    }

    public static void AppendStyles(this IDictionary<string, object?> dictionary, string parentStyles)
    {
        dictionary.AppendValue("style", " ", parentStyles);
    }

    public static void AppendValue(this IDictionary<string, object?> instance, string key, string separator, object value)
    {
        instance[key] = instance.ContainsKey(key)
            ? HtmlEncoder.Default.Encode(instance[key]!.ToString()!) + HtmlEncoder.Default.Encode(separator) + HtmlEncoder.Default.Encode(value.ToString()!)
            : HtmlEncoder.Default.Encode(value.ToString()!);
    }


    public static IHtmlContent ToHtmlAttributes(this IDictionary<string, object?> instance)
    {
        var sb = new StringBuilder();
        foreach (var (key, value) in instance)
        {
            var hasValue = value != null && !string.IsNullOrEmpty(value.ToString());

            sb.Append(" {0}=\"{1}\"".FormatWith(HtmlEncoder.Default.Encode(key), hasValue
                ? HtmlEncoder.Default.Encode(value!.ToString()!)
                : string.Empty));
        }

        return new HtmlString(sb.ToString());
    }

    private static string FormatWith(this string instance, params object[] args)
    {
        return string.Format(CultureInfo.CurrentCulture, instance, args);
    }
}