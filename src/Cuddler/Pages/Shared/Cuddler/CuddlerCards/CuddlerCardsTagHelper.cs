using System.Text.Encodings.Web;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerCards;

[RestrictChildren("cuddler-block", "cuddler-card")]
public class CuddlerCardsTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("eux-CuddlerCards", HtmlEncoder.Default);
    
        await Task.CompletedTask;
    }
}