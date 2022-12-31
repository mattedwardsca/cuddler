using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Ui;
using Cuddler.Web.Api;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.SubmitButton;

public class SubmitButtonTagHelper : BaseTagHelper, ICuddler
{
    public SubmitButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

        ActionComplete = EActionComplete.Reload;
        SubmitButtonType = EButtonType.Success;
    }

    public string? Width { get; set; }

    [Required]
    public EActionComplete ActionComplete { get; set; }

    public string? CallbackFunction { get; set; }

    public string? CancelCallbackFunction { get; set; }

    public string? FormId { get; set; }

    public string? RedirectUrl { get; set; }

    public bool ShowCancel { get; set; } = true;

    public bool ShowDivider { get; set; } = true;

    public CuddlerUri? SubmitApiUrl { get; set; }

    public string SubmitButtonText { get; set; } = "Save";

    [Required]
    public EButtonType SubmitButtonType { get; set; }

    public string? SubmitEvent { get; set; }
}