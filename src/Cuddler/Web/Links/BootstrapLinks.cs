using Microsoft.AspNetCore.Html;

namespace Cuddler.Web.Links;

public static class BootstrapLinks
{
    public static IHtmlContent BootstrapLinkScript(bool useLocal = true)
    {
        if (useLocal)
        {
            const string value = @"<link crossorigin=""anonymous"" href=""/_content/Cuddler.Web/bootstrap/css/bootstrap.min.css"" rel=""stylesheet"">" //
                                 + @"<script src=""/_content/Cuddler.Web/bootstrap/js/bootstrap.bundle.min.js""></script>";
            return new HtmlString(value);
        }

        return new HtmlString(@"<link crossorigin=""anonymous"" href=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css"" integrity=""sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3"" rel=""stylesheet""><script crossorigin=""anonymous"" integrity=""sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p"" src=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js""></script>");
    }
}