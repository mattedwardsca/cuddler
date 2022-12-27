using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerFormFieldsChildren;

public class CuddlerFormFieldsChildrenTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsChildrenTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public CuddlerUri? SaveUrl { get; set; }

    public bool IsTemplate { get; set; }

    public bool AutoSave { get; set; }

    [Required]
    public IEnumerable<Web.Forms.FormField> Fields { get; set; } = null!;
}