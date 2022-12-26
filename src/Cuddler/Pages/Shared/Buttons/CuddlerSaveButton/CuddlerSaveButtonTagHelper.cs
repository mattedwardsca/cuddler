using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Buttons.CuddlerSaveButton;

public class CuddlerSaveButtonTagHelper : BaseTagHelper, ICuddlerButton
{
    public CuddlerSaveButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public CuddlerUri SaveUri { get; set; } = null!;

    [Required]
    public string Label { get; set; } = "Save";

    [Required]
    public string RedirectUrl { get; set; } = null!;
}