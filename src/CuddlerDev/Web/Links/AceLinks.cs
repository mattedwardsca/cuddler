using Microsoft.AspNetCore.Html;

namespace CuddlerDev.Web.Links;

public static class AceLinks
{
    public static IHtmlContent Script()
    {
        return new HtmlString(@"<script src=""/_content/Cuddler/ace/src-min-noconflict/ace.js""></script>");
    }
}