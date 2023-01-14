using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Data.Entities;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.ArchiveButton;

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

    public CuddlerUri? ArchiveUrl { get; set; }

    public EArchiveButtonType ButtonType { get; set; } = EArchiveButtonType.Button;

    [Required]
    public IData Data { get; set; } = null!;

    public string? RedirectUrl { get; set; }

    public CuddlerUri? RestoreUrl { get; set; }
}