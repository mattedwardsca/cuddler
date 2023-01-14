using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerHeader;

[RestrictChildren("cuddler-warning", "cuddler-heading", "cuddler-information")]
public class CuddlerHeaderTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerHeaderTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }
}