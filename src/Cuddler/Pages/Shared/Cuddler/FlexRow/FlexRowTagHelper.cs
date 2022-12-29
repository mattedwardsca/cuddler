using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.FlexRow;

public class FlexRowTagHelper : BaseTagHelper, ICuddler
{
    public FlexRowTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public EFlexLayout Width { get; set; } = EFlexLayout.Full;
}