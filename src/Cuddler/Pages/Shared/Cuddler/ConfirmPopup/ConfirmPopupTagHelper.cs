using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Forms.Ui;
using Cuddler.Web.Api;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ConfirmPopup;

public class ConfirmPopupTagHelper : BaseTagHelper, ICuddler
{
    public ConfirmPopupTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public EActionComplete ActionComplete { get; set; } = EActionComplete.Reload;

    [Required]
    public CuddlerUri ApiUrl { get; set; } = null!;

    [Required]
    public EFontAwesomeIcon ButtonIcon { get; set; } = EFontAwesomeIcon.Edit;

    public string? ButtonText { get; set; }

    [Required]
    public EButtonType ButtonType { get; set; } = EButtonType.Primary;
}