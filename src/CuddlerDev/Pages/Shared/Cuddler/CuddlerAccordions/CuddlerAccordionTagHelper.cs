using System.Text.Json;
using CuddlerDev.Utils;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerAccordions;

public class CuddlerAccordionTagHelper : TagHelper
{
    public string? Id { get; set; }

    public string Text { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;

        if (string.IsNullOrEmpty(Text))
        {
            throw new InvalidOperationException($"{nameof(Text)} cannot be null. Error: 48195b84-848b-4a4d-9633-9be59083d8fe");
        }

        if (string.IsNullOrEmpty(Id))
        {
            Id = WebIdUtil.GetWebId(Text);
        }

        var content = await output.GetChildContentAsync();
        var innerHtml = content.GetContent();

        output.Content.SetHtmlContent(JsonSerializer.Serialize(new CuddlerAccordionTagHelperModel { Id = Id, Text = Text, Value = innerHtml }));
    }
}