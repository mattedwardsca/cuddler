using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerFormFieldsChildren;

public class CuddlerFormFieldsChildrenTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsChildrenTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public CuddlerUri? SaveUrl { get; set; }

    public bool IsTemplate { get; set; }

    public bool AutoSave { get; set; }

    [Required]
    public IEnumerable<Forms.FormField> Fields { get; set; } = null!;
}