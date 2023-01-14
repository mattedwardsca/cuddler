using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerPartial;

// [RestrictChildren("c-data", "cuddler-grid")]
public class CuddlerPartialTagHelper : TagHelper
{
    public ELayout PageLayout { get; set; } = ELayout.Flex;

    public int Spacing { get; set; } = 0;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        await Task.CompletedTask;
    }
}