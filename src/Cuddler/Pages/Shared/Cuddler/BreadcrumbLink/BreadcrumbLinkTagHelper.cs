using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.BreadcrumbLink;

public class BreadcrumbLinkTagHelper : BaseTagHelper, ICuddler
{
    public BreadcrumbLinkTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public string Href { get; set; } = null!;
}