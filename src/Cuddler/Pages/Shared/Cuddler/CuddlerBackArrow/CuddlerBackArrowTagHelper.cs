using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerBackArrow;

public class CuddlerBackArrowTagHelper : TagHelper, ICuddler
{
    public string Href { get; set; } = null!;

    public string Text { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrEmpty(Href))
        {
            throw new InvalidOperationException($"{nameof(Href)} is required in tag {nameof(CuddlerBackArrowTagHelper)}. Error: faf15e7a-f753-4371-99e0-4652c938862a");
        }

        if (string.IsNullOrEmpty(Text))
        {
            throw new InvalidOperationException($"{nameof(Text)} is required in tag {nameof(CuddlerBackArrowTagHelper)}. Error: eb62e124-40c0-4bd0-b062-5b6431126b25");
        }

        await Task.CompletedTask;

        output.TagName = null;
        var html = new HtmlString($"<div class=\"eux-CuddlerBackArrow\"><a class=\"eux-BackArrowWidget-pod\" href=\"{Href}\"><i class=\"fal fa-long-arrow-left\"></i> <span>{Text}</span></a></div>");
        output.Content.SetHtmlContent(html);
    }
}