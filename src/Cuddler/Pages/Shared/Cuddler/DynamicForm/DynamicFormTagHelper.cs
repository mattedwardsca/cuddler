using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Dynamic;
using Cuddler.Web.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.DynamicForm;

public class DynamicFormTagHelper : BaseTagHelper, ICuddler
{
    public DynamicFormTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string CreateUri => $"{PostUrl}?=handler={EDynamicHandler.Create.ToString()}";

    public string DeleteUri => $"{PostUrl}?=handler={EDynamicHandler.Delete.ToString()}";

    [Required]
    public List<Core.Services.Modules.Models.FormField> Fields { get; set; } = null!;

    public EDynamicHandler Handler { get; set; } = EDynamicHandler.Restore;

    public bool IsView { get; set; }

    public string PostUrl { get; set; } = null!;

    public string RestoreUri => $"{PostUrl}?=handler={EDynamicHandler.Restore.ToString()}";

    public string UpdateUri => $"{PostUrl}?=handler={EDynamicHandler.Update.ToString()}";

    public ETagWidth Width { get; set; } = ETagWidth.None;

    public string GetButtonClass()
    {
        switch (Handler)
        {
            case EDynamicHandler.Restore:
                return EButtonTypeHelper.ToString(EButtonType.Warning);

            case EDynamicHandler.Update:
                return EButtonTypeHelper.ToString(EButtonType.Success);

            case EDynamicHandler.Delete:
                return EButtonTypeHelper.ToString(EButtonType.Danger);

            case EDynamicHandler.Create:
                return EButtonTypeHelper.ToString(EButtonType.Success);

            default:
                throw new ArgumentOutOfRangeException(nameof(Handler), Handler, null);
        }
    }

    public string GetButtonText()
    {
        return Handler.ToString();
    }
}