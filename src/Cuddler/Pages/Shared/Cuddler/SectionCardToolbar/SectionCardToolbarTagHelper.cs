using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.SectionCardToolbar;

public class SectionCardToolbarTagHelper : BaseTagHelper, ICuddler
{
    public SectionCardToolbarTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool Justify { get; set; }
}