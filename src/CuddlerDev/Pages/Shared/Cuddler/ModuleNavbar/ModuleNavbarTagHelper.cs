using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.ModuleNavbar;

public class ModuleNavbarTagHelper : BaseTagHelper, ICuddler
{
    public ModuleNavbarTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    [Required]
    public string RootLink { get; set; } = "/";

    [Required]
    public string WebsiteFaviconPath { get; set; } = null!;
}