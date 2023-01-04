using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Toggle;

public class ToggleTagHelper : BaseTagHelper, ICuddler
{
    public ToggleTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool BlockLabel { get; set; }

    public string? Callback { get; set; }

    public bool Checked { get; set; }

    public bool HideLabel { get; set; }

    public string? Label { get; set; }

    [Required]
    public string OffLabel { get; set; } = "Off";

    [Required]
    public string OnLabel { get; set; } = "On";

    public bool RefreshAfter { get; set; }

    public int ToggleWidth { get; set; }

    public CuddlerUri? TurnOffApi { get; set; }

    public CuddlerUri? TurnOnApi { get; set; }
}