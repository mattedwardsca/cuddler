using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerHashEscape;

public class CuddlerHashEscapeTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        string innerHtml;
        try
        {
            innerHtml = output.GetChildContentAsync()
                              .Result.GetContent();
        }
        catch (Exception ex)
        {
            throw new Exception(nameof(CuddlerHashEscapeTagHelper), ex);
        }

        innerHtml = HashEscape(innerHtml)!;

        output.Content.SetHtmlContent(innerHtml);

        await Task.CompletedTask;
    }

    private static string? HashEscape(string? innerHtml)
    {
        innerHtml = innerHtml?.Replace("$(\"#id", "$(\"\\#id");
        innerHtml = innerHtml?.Replace("&#x27;", "'");
        return innerHtml;
    }
}