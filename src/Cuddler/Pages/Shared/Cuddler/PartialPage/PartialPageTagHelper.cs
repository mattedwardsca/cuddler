using System.Text.Encodings.Web;
using Cuddler.Core.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.PartialPage;

[RestrictChildren("section-card")]
public class PartialPageTagHelper : TagHelper
{
    public ELayout PageLayout { get; set; } = ELayout.Flex;

    public int Spacing { get; set; } = 0;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("eux-PartialPage", HtmlEncoder.Default);
        output.AddClass($"m-{Spacing}", HtmlEncoder.Default);

        switch (PageLayout)
        {
            case ELayout.Block:
                output.AddClass("d-block", HtmlEncoder.Default);

                break;

            case ELayout.Flex:
                output.AddClass("me-0", HtmlEncoder.Default);
                output.AddClass("d-flex", HtmlEncoder.Default);
                output.AddClass($"d-flex-gap-{Spacing}", HtmlEncoder.Default);
                output.AddClass("d-flex-wrap", HtmlEncoder.Default);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        await Task.CompletedTask;
    }
}