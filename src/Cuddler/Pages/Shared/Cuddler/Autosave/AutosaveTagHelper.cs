using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Core.Data;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Autosave;

public class AutosaveTagHelper : BaseTagHelper, ICuddler
{
    public AutosaveTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        OnBlur = EBlurAction.None;
    }

    [Required]
    public EBlurAction OnBlur { get; set; }

    [Required]
    public CuddlerUri SaveUrl { get; set; } = null!;

    public string? Script { get; set; }
}