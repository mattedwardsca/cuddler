using System.Text.Encodings.Web;
using System.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerEncode;

public class CuddlerEncodeTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("d-none", HtmlEncoder.Default);
        try
        {
            var innerHtml = output.GetChildContentAsync()
                                  .Result.GetContent();
            output.Content.SetHtmlContent(HttpUtility.HtmlEncode(innerHtml));
        }
        catch (Exception ex)
        {
            throw new Exception(nameof(CuddlerEncodeTagHelper), ex);
        }

        await Task.CompletedTask;
    }
}