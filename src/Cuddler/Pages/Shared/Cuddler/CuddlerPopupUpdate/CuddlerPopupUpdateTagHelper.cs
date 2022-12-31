using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerPopupUpdate;

[RestrictChildren("cuddler-no-children")]
public class CuddlerPopupUpdateTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerPopupUpdateTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public EButtonType ButtonType { get; set; } = EButtonType.Light;

    [Required]
    public string ButtonText { get; set; } = null!;

    [Required]
    public Ui.CuddlerFormModel FormModel { get; set; } = null!;
}