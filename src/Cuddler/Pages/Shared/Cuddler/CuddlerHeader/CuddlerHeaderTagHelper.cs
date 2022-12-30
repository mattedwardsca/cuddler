using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerHeader;

[RestrictChildren("cuddler-warning", "cuddler-heading", "cuddler-information")]
public class CuddlerHeaderTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerHeaderTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }
}