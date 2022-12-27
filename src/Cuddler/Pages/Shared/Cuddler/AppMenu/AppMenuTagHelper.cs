using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Identity;
using Cuddler.Web.Modules;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.AppMenu;

public class AppMenuTagHelper : BaseTagHelper, ICuddler
{
    public AppMenuTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool HideIcon { get; set; }

    [Required]
    public IClientApp SegmentApp { get; set; } = null!;

    [Required]
    public IAccount Account { get; set; } = null!;
}