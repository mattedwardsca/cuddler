using System.Text.Json;
using Cuddler.Utils;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerTabs;

[RestrictChildren("cuddler-no-tags")]
public class CuddlerTabTagHelper : TagHelper
{
    public string Value { get; set; } = null!;

    public string? Id { get; set; }

    public string Text { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;

        if (string.IsNullOrEmpty(Value))
        {
            throw new InvalidOperationException($"Tab {nameof(Value)} cannot be null. Error: e719eba3-4e3e-4dfe-a847-9f855667e2fc");
        }

        if (string.IsNullOrEmpty(Text))
        {
            throw new InvalidOperationException($"Tab {nameof(Text)} cannot be null. Error: f0949daa-5217-4143-8469-7f295daf823f");
        }

        if (string.IsNullOrEmpty(Id))
        {
            Id = WebIdUtil.GetWebId(Text);
        }

        output.Content.SetHtmlContent(JsonSerializer.Serialize(this));

        await Task.CompletedTask;
    }
}