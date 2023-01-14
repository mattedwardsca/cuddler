using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.ExportCsvButton;

public class ExportCsvButtonTagHelper : BaseTagHelper, ICuddler
{
    public ExportCsvButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public CuddlerUri ExportUri { get; set; } = null!;

    public string? ClientTemplateId { get; set; }

    public bool IsTemplate { get; set; }
}