using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Data.Entities;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Modules;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Portal;

public class PortalTagHelper : BaseTagHelper, ICuddler
{
    public PortalTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public IAccount Account { get; set; } = null!;

    [Required]
    public IApp SegmentApp { get; set; } = null!;

    [Required]
    public string WebsiteFaviconPath { get; set; } = null!;
}