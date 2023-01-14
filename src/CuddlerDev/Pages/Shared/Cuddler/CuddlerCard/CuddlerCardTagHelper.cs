using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Pages.Shared.Cuddler.ActionMenu;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerCard;

[RestrictChildren("c-data", "partial", "paper", "cuddler-information", "cuddler-autosave", "cuddler-autosave-button", "cuddler-back-arrow", "callout")]
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