using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Core.Services.Modules.Models;
using Cuddler.Data.Entities;
using Cuddler.Web.BaseTagHelpers;
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
    public List<string> PinnedAppIdsForUser { get; set; } = null!;

    [Required]
    public IClientApp SegmentApp { get; set; } = null!;

    [Required]
    public string WebsiteFaviconPath { get; set; } = null!;
}