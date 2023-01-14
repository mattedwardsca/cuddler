using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerPage;

[RestrictChildren("cuddler-header", "cuddler-body")]
public class CuddlerPageTagHelper : TagHelper
{
    public bool NoHeading { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("eux-CuddlerPage", HtmlEncoder.Default);

        if (NoHeading)
        {
            output.AddClass("eux-noheader", HtmlEncoder.Default);
        }

        await Task.CompletedTask;
    }
}