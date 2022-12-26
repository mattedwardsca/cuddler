using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Core.Data;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
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