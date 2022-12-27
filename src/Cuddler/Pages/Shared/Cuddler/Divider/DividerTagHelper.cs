using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Divider;

public class DividerTagHelper : BaseTagHelper, ICuddler
{
    public DividerTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

        Height = 2;
        Color = "var(--ColorFive)";
    }

    public string Color { get; set; }

    public int Height { get; set; }

    public int? Width { get; set; }
}