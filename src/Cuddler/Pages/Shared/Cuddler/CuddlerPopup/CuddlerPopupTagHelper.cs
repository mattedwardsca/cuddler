using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerPopup;

public class CuddlerPopupTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerPopupTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        ButtonIcon = EFontAwesomeIcon.Window;
    }

    [Required]
    public EActionComplete ActionComplete { get; set; }

    [Required]
    public EFontAwesomeIcon ButtonIcon { get; set; }

    public string? ButtonText { get; set; }

    [Required]
    public EButtonType ButtonType { get; set; } = EButtonType.Primary;

    public string? CloseEvent { get; set; }

    public string? Id { get; set; }

    public string? PopupEvent { get; set; }

    public string? PopupHref { get; set; }

    public string? RedirectUrl { get; set; }

    public CuddlerUri? SubmitApiUrl { get; set; }

    public List<SelectListItem>? Filter { get; set; }

    public bool SubmitButtonHide { get; set; }

    public string? SubmitButtonText { get; set; }

    [Required]
    public EButtonType SubmitButtonType { get; set; } = EButtonType.Success;

    public string? SubmitEvent { get; set; }

    public string? PopupTitle { get; set; }
}