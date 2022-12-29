using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Head;

public class HeadTagHelper : BaseTagHelper, ICuddler
{
    public HeadTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }
}