using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerBlock;

public class CuddlerBlockTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerBlockTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    public string? SkeletonHeight { get; set; }

    public string? Src { get; set; }

    public ETagWidth Width { get; set; } = ETagWidth.None;
}