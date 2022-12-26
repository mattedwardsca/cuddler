using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Core.Models;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
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