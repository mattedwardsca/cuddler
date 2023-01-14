using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerGrid;

public class CuddlerGridTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerGridTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public CuddlerUri? RestoreApi { get; set; }

    public CuddlerUri? DeleteApi { get; set; }

    public string? DetailsUrl { get; set; }

    [Required]
    public GridCuddlerFormFields FormFields { get; set; } = null!;

    public string? GridId { get; set; }

    public bool HideHeader { get; set; }

    [Required]
    public CuddlerUri Query { get; set; } = null!;

    public bool Pageable { get; set; }
}