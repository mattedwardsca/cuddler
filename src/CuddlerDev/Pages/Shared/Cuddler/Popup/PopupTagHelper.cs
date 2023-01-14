using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.Popup;

public class PopupTagHelper : BaseTagHelper, ICuddler
{
    public PopupTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        ActionComplete = EActionComplete.Nothing;
    }

    public EActionComplete ActionComplete { get; set; }

    public string? EventName { get; set; }

    [Required]
    public string Id { get; set; } = null!;

    public string? PopupTitle { get; set; }
}