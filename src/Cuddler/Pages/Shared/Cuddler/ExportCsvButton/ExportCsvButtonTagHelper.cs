using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ExportCsvButton;

public class ExportCsvButtonTagHelper : BaseTagHelper, ICuddler
{
    public ExportCsvButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public CuddlerUri ExportUri { get; set; } = null!;

    [Required]
    public string ClientTemplateId { get; set; } = null!;
}