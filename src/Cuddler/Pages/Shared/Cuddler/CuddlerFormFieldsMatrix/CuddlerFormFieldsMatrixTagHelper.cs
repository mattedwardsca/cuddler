using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
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
    public IEnumerable<Core.Services.Modules.Models.FormField> Fields { get; set; } = null!;
}