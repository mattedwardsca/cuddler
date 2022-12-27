using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Data.Entities;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ArchiveButton;

public enum EArchiveButtonType
{
    Button,
    Link,
    ActionMenu
}

public class ArchiveButtonTagHelper : BaseTagHelper, ICuddler
{
    public ArchiveButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public EActionComplete ActionComplete { get; set; }

    public string? ArchiveReason { get; set; }

    public string? ArchiveText { get; set; }

    public CuddlerUri? ArchiveUrl { get; set; }

    public EArchiveButtonType ButtonType { get; set; } = EArchiveButtonType.Button;

    [Required]
    public IData Data { get; set; } = null!;

    public bool HideArchiveReason { get; set; }

    public string? OverrideName { get; set; }

    public string? RedirectUrl { get; set; }

    public CuddlerUri? RestoreUrl { get; set; }

    public string? UnarchiveText { get; set; }
}