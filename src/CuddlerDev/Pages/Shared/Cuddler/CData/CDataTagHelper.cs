using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CData;

public class CDataTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        await Task.CompletedTask;
    }
}