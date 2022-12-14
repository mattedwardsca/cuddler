using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Pages.Shared.Cuddler.ActionMenu;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerCard;

[RestrictChildren("c-data", "partial", "paper")]
public class CuddlerCardTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerCardTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string? WebId { get; set; }

    public ActionMenuItems? ActionMenu { get; set; }

    public bool Border { get; set; }

    public bool Center { get; set; }

    public bool FullHeight { get; set; }

    public bool IsToolbar { get; set; }

    public string? LoadingId { get; set; }

    public string? MinHeight { get; set; }

    public bool Skeleton { get; set; }

    public string? Src { get; set; }

    public string? Title { get; set; }

    public ETagWidth Width { get; set; } = ETagWidth.Grow;
}