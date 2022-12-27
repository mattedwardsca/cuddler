using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Image;

public class ImageTagHelper : BaseTagHelper, ICuddler
{
    public ImageTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public EImageDimensions Dimensions { get; set; }

    [Required]
    public string FullsizeUrl { get; set; } = null!;

    public bool Lighthouse { get; set; }

    [Required]
    public string PreviewUrl { get; set; } = null!;

    public EImageSize Size { get; set; }
}