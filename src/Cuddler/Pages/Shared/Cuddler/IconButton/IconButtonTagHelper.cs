using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.IconButton;

public class IconButtonTagHelper : BaseTagHelper, ICuddler
{
    public IconButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public EFontAwesomeIcon ButtonIcon { get; set; } = EFontAwesomeIcon.None;

    public bool Disabled { get; set; }

    public bool HideLabel { get; set; }

    public string? Href { get; set; }

    public string? OverrideIcon { get; set; }

    public EFontAwesomeSize Size { get; set; } = EFontAwesomeSize.Default;
}