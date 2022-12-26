using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.PageView;

public class PageViewTagHelper : BaseTagHelper, ICuddler
{
    public PageViewTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public ViewModel ViewModel { get; set; } = null!;
}