using System.Web;
using Microsoft.AspNetCore.Html;

namespace Cuddler.Links;

public static class CuddlerLinks
{
    public const string FaviconSrc = "/_content/Cuddler/boost/Boost-favicon.svg";
    public const string LogoSrc = "/_content/Cuddler/boost/Boost-logo.svg";

    public static string FaviconEncodedImg(int width = 20, string title = "Boost")
    {
        return HttpUtility.HtmlEncode($@"<img src='{FaviconSrc}' style='width:{width}px;' /> {title} ");
    }

    public static IHtmlContent FaviconImg(int width = 20)
    {
        return new HtmlString($@"<img src=\'{FaviconSrc}\' style='width:{width}px;' /> ");
    }

    public static IHtmlContent LogoImg(int width = 100)
    {
        return new HtmlString($@"<img src=\'{LogoSrc}\' style='width:{width}px;' /> ");
    }
}