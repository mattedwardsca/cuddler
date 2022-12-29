using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Badge;

public class BadgeTagHelper : BaseTagHelper, ICuddler
{
    public BadgeTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        FontSize = EFontSize.TwoX;
        BadgeType = EBadgeType.Info;
    }

    public EBadgeType BadgeType { get; set; }

    public EFontSize FontSize { get; set; }

    public string? Href { get; set; }
}