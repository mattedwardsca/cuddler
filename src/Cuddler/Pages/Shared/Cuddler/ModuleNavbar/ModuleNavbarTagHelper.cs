using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ModuleNavbar;

public class ModuleNavbarTagHelper : BaseTagHelper, ICuddler
{
    public ModuleNavbarTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    [Required]
    public string RootLink { get; set; } = "/";

    [Required]
    public List<string> PinnedAppIdsForUser { get; set; } = null!;

    [Required]
    public string WebsiteFaviconPath { get; set; } = null!;
}