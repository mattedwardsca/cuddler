using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Data.Entities;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Modules;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.AppMenu;

public class AppMenuTagHelper : BaseTagHelper, ICuddler
{
    public AppMenuTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool HideIcon { get; set; }

    [Required]
    public IApp SegmentApp { get; set; } = null!;

    [Required]
    public IAccount Account { get; set; } = null!;
}