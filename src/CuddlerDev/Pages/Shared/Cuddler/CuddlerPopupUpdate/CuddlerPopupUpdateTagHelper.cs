using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerPopupUpdate;

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
    public CuddlerUri FormAction { get; set; } = null!;

    [Required]
    public List<Forms.FormField> FormFields { get; set; } = null!;
}