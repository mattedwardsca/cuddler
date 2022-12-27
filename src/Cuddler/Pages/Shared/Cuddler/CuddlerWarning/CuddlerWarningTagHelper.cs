using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Data.Entities;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerWarning;

public class CuddlerWarningTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerWarningTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    [Required]
    public IData Data { get; set; } = null!;

    public string? OverrideName { get; set; }

    public string? OverrideText { get; set; }
}