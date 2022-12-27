using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Forms;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerPopupUpdate;

public class CuddlerPopupUpdateTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerPopupUpdateTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public EButtonType ButtonType { get; set; } = EButtonType.Light;

    public string? OverridePopupTitle { get; set; }

    public bool HideButtonText { get; set; }

    [Required]
    public CuddlerFields UpdateModel { get; set; } = null!;

    [Required]
    public CuddlerUri SubmitApiUrl { get; set; } = null!;
}