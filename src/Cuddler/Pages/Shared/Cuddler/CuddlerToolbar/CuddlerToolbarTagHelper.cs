using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerToolbar;

[RestrictChildren("archive-button", "popup-editor", "cuddler-link", "cuddler-confirm", "cuddler-popup-update", "partial", "print-button")]
public class CuddlerToolbarTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerToolbarTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }
}