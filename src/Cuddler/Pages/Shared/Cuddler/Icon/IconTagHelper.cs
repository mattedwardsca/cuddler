using System.Text.Encodings.Web;
using Cuddler.Core.Data;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Icon;

public class IconTagHelper : BaseTagHelper, ICuddler
{
    public IconTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        Type = EFontAwesomeIcon.None;
        Size = EFontAwesomeSize.Default;
    }

    public EFontAwesomeSize Size { get; set; }

    public EFontAwesomeIcon Type { get; set; }
}