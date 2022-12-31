using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerUpdate;

public class CuddlerUpdateTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerUpdateTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public Ui.CuddlerFormModel FormModel { get; set; } = null!;

    public bool AutoSave { get; set; } = true;
}