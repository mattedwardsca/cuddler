using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerSubmitButton;

public class CuddlerSubmitButtonTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerSubmitButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

        ActionComplete = EActionComplete.Reload;
        SubmitButtonType = EButtonType.Success;
    }

    public EActionComplete ActionComplete { get; set; }

    public string? CallbackFunction { get; set; }

    public string? CancelCallbackFunction { get; set; }

    public string? FormId { get; set; }

    public List<SelectListItem>? Filter { get; set; }

    public string? RedirectUrl { get; set; }

    public bool ShowCancel { get; set; } = true;

    public bool ShowDivider { get; set; } = true;

    public CuddlerUri? SubmitApiUrl { get; set; }

    public string SubmitButtonText { get; set; } = "Save";

    public EButtonType SubmitButtonType { get; set; }

    public string? SubmitEvent { get; set; }
}