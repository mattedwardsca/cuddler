using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ButtonLink;

public class ButtonLinkTagHelper : BaseTagHelper, ICuddler
{
    public ButtonLinkTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public EFontAwesomeIcon ButtonIcon { get; set; }

    public EButtonType ButtonType { get; set; }

    public string? ButtonWidth { get; set; }

    [Required]
    public string Href { get; set; } = null!;

    public string? Id { get; set; }

    public string? Target { get; set; }
}