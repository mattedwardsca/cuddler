using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.PageLoader;

public class PageLoaderTagHelper : BaseTagHelper, ICuddler
{
    public PageLoaderTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public int Delay { get; set; } = 500;
}