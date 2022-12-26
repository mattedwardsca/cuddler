using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
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
    public IEnumerable<Core.Forms.FormField> Fields { get; set; } = null!;
}