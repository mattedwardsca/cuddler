using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerCards;

[RestrictChildren("cuddler-block", "cuddler-card")]
public class CuddlerCardsTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerCardsTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }
}