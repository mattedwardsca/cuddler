using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerFormFields;

public class CuddlerFormFieldsTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public List<Forms.FormField> Fields { get; set; } = null!;

    public bool IsTemplate { get; set; }

    public bool IsView { get; set; }

    public bool AutoSave { get; set; }

    public CuddlerUri? SaveUrl { get; set; }
}