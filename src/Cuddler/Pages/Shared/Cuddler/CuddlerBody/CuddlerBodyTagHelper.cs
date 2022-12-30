using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerBody;

[RestrictChildren("cuddler-cards", "cuddler-tabs", "cuddler-accordions", "cuddler-kanban")]
public class CuddlerBodyTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerBodyTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }
}