using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerLink;

public class CuddlerLinkTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerLinkTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
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