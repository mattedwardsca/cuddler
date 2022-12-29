using System.Text.Encodings.Web;
using Cuddler.Web.Helpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerPageContentSection;

[RestrictChildren("cuddler-block", "section-card", "style", "script", "section-tabs", "activity-cards", "partial-loader")]
public class CuddlerPageContentSectionTagHelper : TagHelper
{
    public ELayout PageLayout { get; set; } = ELayout.Flex;

    public int Spacing { get; set; } = 0;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("eux-CuddlerPageContentSection", HtmlEncoder.Default);
        output.AddClass($"m-{Spacing}", HtmlEncoder.Default);

        switch (PageLayout)
        {

            case ELayout.Block:
                output.AddClass("d-block", HtmlEncoder.Default);

                break;
            case ELayout.Flex:
                output.AddClass("me-0", HtmlEncoder.Default);
                output.AddClass("d-flex", HtmlEncoder.Default);

                if (Spacing > 0)
                {
                    output.AddClass($"d-flex-gap-{Spacing}", HtmlEncoder.Default);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await Task.CompletedTask;
    }
}