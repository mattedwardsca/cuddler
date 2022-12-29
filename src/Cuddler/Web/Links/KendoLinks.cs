using Microsoft.AspNetCore.Html;

namespace Cuddler.Web.Links;

public static class KendoLinks
{
    public static IHtmlContent GetHeaderHtml()
    {
        return new HtmlString(@"
            <link href=""/_content/Cuddler.Web/kendo/styles/kendo.common-bootstrap.min.css"" rel=""stylesheet"" />
            <link href=""/_content/Cuddler.Web/kendo/styles/kendo.bootstrap.min.css"" rel=""stylesheet"" />
            <script src=""/_content/Cuddler.Web/kendo/js/jszip.min.js""></script>
            <script src=""/_content/Cuddler.Web/kendo/js/pako_deflate.min.js""></script>
            <script src=""/_content/Cuddler.Web/kendo/js/kendo.all.min.js""></script>
            <script src=""/_content/Cuddler.Web/kendo/js/kendo.aspnetmvc.min.js""></script>
            <script src=""/_content/Cuddler.Web/kendo/js/kendo.timezones.min.js""></script>");
    }
}