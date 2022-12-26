using System.Text.Encodings.Web;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.FormTooltip;

public class FormTooltipTagHelper : BaseTagHelper, ICuddler
{
    public FormTooltipTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string? Title { get; set; }
}