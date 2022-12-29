using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerPageHeading;

[RestrictChildren("cuddler-warning", "cuddler-heading", "cuddler-information")]
public class CuddlerPageHeadingTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerPageHeadingTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool HasBottomBorder { get; set; }
}