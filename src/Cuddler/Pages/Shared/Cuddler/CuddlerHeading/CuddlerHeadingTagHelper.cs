using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Data.Entities;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Forms.Ui;
using Cuddler.Pages.Shared.Cuddler.ActionMenu;
using Cuddler.Web.Api;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerHeading;

[RestrictChildren("cuddler-toolbar")]
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
}