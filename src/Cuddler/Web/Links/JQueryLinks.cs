using Microsoft.AspNetCore.Html;

namespace Cuddler.Links;

public static class JQueryLinks
{
    public static IHtmlContent JQueryLibraryScript(bool useLocal = true)
    {
        if (useLocal)
        {
            return new HtmlString(@"<script src=""/_content/Cuddler/jquery/jquery-3.6.0.min.js""></script>");
        }

        return new HtmlString(@"<script crossorigin=""anonymous"" integrity=""sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4="" src=""https://code.jquery.com/jquery-3.6.0.min.js""></script>");
    }

    public static IHtmlContent JsCookieScript()
    {
        return new HtmlString(@"<script src=""/_content/Cuddler/js-cookie/js.cookie.min.js""></script>");
    }

    public static IHtmlContent Lightbox2LinkScript()
    {
        return new HtmlString(@"<link  rel=""stylesheet"" href=""/_content/Cuddler/lightbox2/css/lightbox.min.css""/><script src=""/_content/Cuddler/lightbox2/js/lightbox.min.js""></script>");
    }

    public static IHtmlContent SignaturePadScript()
    {
        return new HtmlString(@"<script src=""/_content/Cuddler/signature_pad/signature_pad.min.js""></script>");
    }
}