using System.Text.Json;
using CuddlerDev.Utils;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.Wizard;

public class WizardStepTagHelper : TagHelper
{
    public string StepTitle { get; set; } = null!;

    public string StepUrl { get; set; } = null!;

    public string? TabId { get; set; }

    public bool ShowTitle { get; set; } = true;

    public bool IsDone { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrEmpty(StepTitle))
        {
            throw new InvalidOperationException("ff3bf4a5-11d8-470f-b6c0-d6e54de28a63");
        }

        if (string.IsNullOrEmpty(StepUrl))
        {
            throw new InvalidOperationException("658540e5-fe42-4cda-911e-30bf2e2480d1");
        }

        output.TagName = null;

        var stepDto = new WizardStep
        {
            StepUrl = StepUrl,
            StepTitle = StepTitle,
            Id = TabId ?? WebIdUtil.GetWebId(StepTitle),
            ShowTitle = ShowTitle,
            IsDone = IsDone
        };

        output.Content.SetHtmlContent(JsonSerializer.Serialize(stepDto));

        await Task.CompletedTask;
    }
}