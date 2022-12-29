using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ToggleCookie;

public class ToggleCookieTagHelper : BaseTagHelper, ICuddler
{
    public ToggleCookieTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool BlockLabel { get; set; }

    public bool Flip { get; set; }

    public bool HideLabel { get; set; }

    public string? Label { get; set; }

    [Required]
    public string OffLabel { get; set; } = "Off";

    [Required]
    public string OnLabel { get; set; } = "On";

    [Required]
    public string ParamName { get; set; } = null!;

    public bool RefreshAfter { get; set; } = true;

    public int ToggleWidth { get; set; }

    public string? TurnOffApi { get; set; }

    public string? TurnOnApi { get; set; }
}