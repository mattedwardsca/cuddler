using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerFormFields;

public class CuddlerFormFieldsTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool AutoSave { get; set; }

    [Required]
    public List<Web.Forms.FormField> Fields { get; set; } = null!;

    public bool IsTemplate { get; set; }
    
    public bool IsView { get; set; }

    public CuddlerUri? SaveUrl { get; set; }
}