using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Kanban;

public class KanbanTagHelper : BaseTagHelper
{
    public KanbanTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    public string? CardDetailsUrl { get; set; }

    [Required]
    public FactoryBoardModel FactoryBoard { get; set; } = null!;

    [Required]
    public CuddlerUri StationApi { get; set; } = null!; // = "/Apis/Kanban/ListKanbanCards";

    public static EBadgeType ToBadge(EStationType state)
    {
        return state switch
        {
            EStationType.Green => EBadgeType.Success,
            EStationType.DarkBlue => EBadgeType.Primary,
            EStationType.LightBlue => EBadgeType.Info,
            EStationType.Gray => EBadgeType.Secondary,
            EStationType.Yellow => EBadgeType.Warning,
            var _ => EBadgeType.Secondary
        };
    }
}