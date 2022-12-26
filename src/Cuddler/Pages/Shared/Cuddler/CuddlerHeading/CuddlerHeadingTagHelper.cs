using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Core.Models;
using Cuddler.Pages.Shared.Cuddler.ActionMenu;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerHeading;

[RestrictChildren("a", "action-menu", "popup-editor", "today", "cuddler-form-create", "cuddler-popup-update", "cuddler-archive-button", "button-link", "cuddler-popup-form", "button-link", "archive-button", "client-create-button", "partial", "autosave", "create-project-button", "help")]
public class CuddlerHeadingTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerHeadingTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public IHasName? Data { get; set; }

    public CuddlerUri? UpdateNameApi { get; set; }

    public IHtmlContent? Information { get; set; }

    public string? Tag { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public CuddlerUri? ArchiveUri { get; set; }

    public CuddlerUri? RestoreUri { get; set; }

    public ActionMenuItems? Menu { get; set; }
}