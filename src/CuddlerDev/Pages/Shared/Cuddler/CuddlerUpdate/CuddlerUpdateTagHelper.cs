using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerUpdate;

public class CuddlerUpdateTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerUpdateTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public CuddlerUri? FormAction { get; set; }

    [Required]
    public List<Forms.FormField> FormFields { get; set; } = null!;

    public bool AutoSave { get; set; } = true;
}