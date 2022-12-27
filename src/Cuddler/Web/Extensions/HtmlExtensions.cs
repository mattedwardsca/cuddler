using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Cuddler.Web.Extensions;

public static class HtmlExtensions
{
    public static string? ToNullableString(this IHtmlContent htmlContent)
    {
        var writer = new StringWriter();
        htmlContent.WriteTo(writer, HtmlEncoder.Default);
        var overrideHtml = writer.ToString()
                                 .Trim();

        return string.IsNullOrEmpty(overrideHtml)
            ? null
            : overrideHtml;
    }

    public static IHtmlContent ToHtml(this IHtmlContent htmlContent)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (htmlContent == null)
        {
            return new HtmlString("");
        }

        var writer = new StringWriter();
        htmlContent.WriteTo(writer, HtmlEncoder.Default);
        var overrideHtml = writer.ToString()
                                 .Trim();

        return new HtmlString(overrideHtml);
    }
}