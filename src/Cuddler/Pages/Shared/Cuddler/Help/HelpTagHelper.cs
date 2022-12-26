using System.Text.Encodings.Web;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Help;

public class HelpTagHelper : BaseTagHelper, ICuddler
{
    public HelpTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }
}