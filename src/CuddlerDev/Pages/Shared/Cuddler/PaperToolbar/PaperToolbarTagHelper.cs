using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.PaperToolbar;

public class PaperToolbarTagHelper : BaseTagHelper, ICuddler
{
    public PaperToolbarTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool Justify { get; set; }
}