using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.PartialLoader;

public class PartialLoaderTagHelper : BaseTagHelper, ICuddler
{
    public PartialLoaderTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public string Src { get; set; } = null!;

    [Required]
    public string WebId { get; set; } = null!;
}