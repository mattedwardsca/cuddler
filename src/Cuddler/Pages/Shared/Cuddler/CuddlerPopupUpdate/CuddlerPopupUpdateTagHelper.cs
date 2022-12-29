using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Web.Api;
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
    public Forms.CuddlerFormFields UpdateModel { get; set; } = null!;

    [Required]
    public CuddlerUri SubmitApiUrl { get; set; } = null!;
}