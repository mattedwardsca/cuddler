using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace Cuddler.Pages.Shared.Cuddler.PaperToolbar;

public class PaperToolbarTagHelper : BaseTagHelper, ICuddler
{
    public PaperToolbarTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool Justify { get; set; }
}