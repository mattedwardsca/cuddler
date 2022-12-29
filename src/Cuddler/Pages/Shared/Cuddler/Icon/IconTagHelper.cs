using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Icon;

public class IconTagHelper : BaseTagHelper, ICuddler
{
    public IconTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        Type = EFontAwesomeIcon.None;
        Size = EFontAwesomeSize.Default;
    }

    public EFontAwesomeSize Size { get; set; }

    public EFontAwesomeIcon Type { get; set; }
}