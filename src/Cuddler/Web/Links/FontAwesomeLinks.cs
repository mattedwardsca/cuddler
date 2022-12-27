using Microsoft.AspNetCore.Html;

namespace Cuddler.Web.Links;

public static class FontAwesomeLinks
{
    public static IHtmlContent FontAwesomeLink(bool useLocal = true)
    {
        if (useLocal)
        {
            return new HtmlString(@"<link href=""/_content/BoostDC.Ui/fontawesome/css/all.min.css"" rel=""stylesheet"">");
        }

        return new HtmlString(@"<script crossorigin=""anonymous"" src=""https://kit.fontawesome.com/185af9dfd8.js""></script>");
    }
}