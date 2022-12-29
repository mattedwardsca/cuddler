using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.PaperHeader;

public class PaperHeaderTagHelper : BaseTagHelper, ICuddler
{
    public PaperHeaderTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string? BookTitle { get; set; }

    public string? PageTitle { get; set; }
}