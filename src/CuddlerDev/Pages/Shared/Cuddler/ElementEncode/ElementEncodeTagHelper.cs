using System.Text.Encodings.Web;
using System.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.ElementEncode;

public class ElementEncodeTagHelper : TagHelper
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
            throw new Exception(nameof(ElementEncodeTagHelper), ex);
        }

        await Task.CompletedTask;
    }
}