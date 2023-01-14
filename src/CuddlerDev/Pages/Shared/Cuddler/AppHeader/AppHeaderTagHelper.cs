using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Data.Entities;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Modules;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.AppHeader;

public class AppHeaderTagHelper : BaseTagHelper, ICuddler
{
    public AppHeaderTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

    }

    [Required]
    public IApp SegmentApp { get; set; } = null!;

    [Required]
    public IAccount Account { get; set; } = null!;
}