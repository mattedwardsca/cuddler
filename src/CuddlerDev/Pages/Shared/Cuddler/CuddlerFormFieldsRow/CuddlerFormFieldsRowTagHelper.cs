using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerFormFieldsRow;

public class CuddlerFormFieldsRowTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsRowTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public Forms.FormField FormField { get; set; } = null!;
}