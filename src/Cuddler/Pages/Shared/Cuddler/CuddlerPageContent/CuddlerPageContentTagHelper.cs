using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerPageContent;

[RestrictChildren("cuddler-page-content-section", "cuddler-tabs", "content-accordion", "kanban")]
public class CuddlerPageContentTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerPageContentTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }
}