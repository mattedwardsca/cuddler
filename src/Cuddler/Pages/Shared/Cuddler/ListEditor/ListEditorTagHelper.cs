using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ListEditor;

public class ListEditorTagHelper : BaseTagHelper, ICuddler
{
    public ListEditorTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    public EListEditorActionComplete ActionComplete { get; set; } = EListEditorActionComplete.Reload;

    public bool AllowSort { get; set; }

    [Required]
    public CuddlerUri ArchiveApi { get; set; } = null!;

    [Required]
    public CuddlerUri CreateApi { get; set; } = null!;

    public List<Forms.FormField>? CreateModel { get; set; }

    public string? OrderBy { get; set; }

    [Required]
    public string PluralText { get; set; } = null!;

    [Required]
    public CuddlerUri ReadApi { get; set; } = null!;

    public bool ShowArchive { get; set; }

    [Required]
    public string SingleText { get; set; } = null!;

    public CuddlerUri? SortApi { get; set; }

    [Required]
    public CuddlerUri UpdateApi { get; set; } = null!;

    public List<Forms.FormField>? UpdateModel { get; set; }
}