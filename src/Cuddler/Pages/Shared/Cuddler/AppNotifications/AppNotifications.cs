using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.AppNotifications;

public class AppNotifications : BaseTagHelper
{
    public AppNotifications(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }
}