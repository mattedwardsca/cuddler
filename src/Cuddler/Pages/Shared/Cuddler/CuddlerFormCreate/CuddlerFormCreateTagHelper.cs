using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerFormCreate;

public class CuddlerFormCreateTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormCreateTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public EActionComplete ActionComplete { get; set; } = EActionComplete.Details;

    [Required]
    public Forms.CuddlerFormFields CreateModel { get; set; } = null!;

    public string? DetailsUrl { get; set; }

    public string? OverrideButtonText { get; set; }

    public string? PopupEvent { get; set; }

    [Required]
    public CuddlerUri SubmitApiUrl { get; set; } = null!;
}