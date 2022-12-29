using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Web.Api;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerFormFieldsMatrix;

public class CuddlerFormFieldsMatrixTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsMatrixTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public CuddlerUri? SaveUrl { get; set; }

    public bool AutoSave { get; set; }

    [Required]
    public IEnumerable<Forms.FormField> Fields { get; set; } = null!;
}