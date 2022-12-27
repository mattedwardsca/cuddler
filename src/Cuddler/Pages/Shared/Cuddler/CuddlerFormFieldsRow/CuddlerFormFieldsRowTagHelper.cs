using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerFormFieldsRow;

public class CuddlerFormFieldsRowTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsRowTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public Core.Services.Modules.Models.FormField FormField { get; set; } = null!;
}