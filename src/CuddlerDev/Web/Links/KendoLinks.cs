using Microsoft.AspNetCore.Html;

namespace CuddlerDev.Web.Links;

public static class KendoLinks
{
    public static IHtmlContent GetHeaderHtml()
    {
        return new HtmlString(@"<link href=""/_content/Cuddler/kendo/styles/kendo.common-bootstrap.min.css"" rel=""stylesheet"" />
    <link href=""/_content/Cuddler/kendo/styles/kendo.bootstrap.min.css"" rel=""stylesheet"" />
    <script src=""/_content/Cuddler/kendo/js/jszip.min.js""></script>
    <script src=""/_content/Cuddler/kendo/js/pako_deflate.min.js""></script>
    <script src=""/_content/Cuddler/kendo/js/kendo.all.min.js""></script>
    <script src=""/_content/Cuddler/kendo/js/kendo.aspnetmvc.min.js""></script>
    <script src=""/_content/Cuddler/kendo/js/kendo.timezones.min.js""></script>");
    }
}