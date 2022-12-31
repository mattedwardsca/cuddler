using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Ui;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerPopupForm;

//[RestrictChildren("p", "div")]
public class CuddlerPopupFormTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerPopupFormTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    [Required]
    public EActionComplete ActionComplete { get; set; } = EActionComplete.Reload;

    [Required]
    public EFontAwesomeIcon ButtonIcon { get; set; } = EFontAwesomeIcon.None;

    public string? ButtonText { get; set; }

    [Required]
    public EButtonType ButtonType { get; set; } = EButtonType.Primary;

    public string? CloseEventName { get; set; }

    public string? ConfirmedText { get; set; }

    public string? DetailsUrl { get; set; }

    [Required]
    public List<Forms.FormField> Fields { get; set; } = null!;

    public string? PopupEvent { get; set; }

    public string? PopupTitle { get; set; }

    [Required]
    public CuddlerUri SubmitApiUrl { get; set; } = null!;

    [Required]
    public string SubmitButtonText { get; set; } = null!;

    [Required]
    public EButtonType SubmitButtonType { get; set; } = EButtonType.Success;

    public string? SubmitEvent { get; set; }
}