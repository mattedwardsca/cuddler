using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Modules;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerGrid;

public class CuddlerGridTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerGridTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string? RestoreApi { get; set; }

    public string? DeleteApi { get; set; }

    public string? DetailsUrl { get; set; }

    [Required]
    public GridCuddlerFields Fields { get; set; } = null!;

    public string? GridId { get; set; }

    public bool HideHeader { get; set; }

    [Required]
    public CuddlerUri Query { get; set; } = null!;

    public bool Pageable { get; set; }
}