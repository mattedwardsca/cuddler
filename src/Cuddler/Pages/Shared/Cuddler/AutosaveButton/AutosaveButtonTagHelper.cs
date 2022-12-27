using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.AutosaveButton;

public class AutosaveButtonTagHelper : BaseTagHelper, ICuddler
{
    public AutosaveButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }
}