using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerAutosave;

public class CuddlerAutosaveTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerAutosaveTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        OnBlur = EBlurAction.None;
    }

    [Required]
    public EBlurAction OnBlur { get; set; }

    [Required]
    public CuddlerUri SaveUrl { get; set; } = null!;

    public string? Script { get; set; }
}