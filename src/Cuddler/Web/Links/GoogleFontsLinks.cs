using Microsoft.AspNetCore.Html;

namespace Cuddler.Web.Links;

public static class GoogleFontsLinks
{
    public static IHtmlContent GoogleFontLink(bool useLocal = true)
    {
        if (useLocal)
        {
            return new HtmlString(@"<link href=""/_content/Cuddler/Karla/Karla.css?display=swap"" rel=""stylesheet"">");
        }

        return new HtmlString(@"<link href=""https://fonts.googleapis.com/css2?family=Karla:ital,wght@0,400;0,700;1,400;1,700&display=swap"" rel=""stylesheet"">");
    }
}