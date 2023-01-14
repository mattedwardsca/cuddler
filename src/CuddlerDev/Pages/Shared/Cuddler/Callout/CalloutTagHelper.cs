using System.Text;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.Callout;

public class CalloutTagHelper : TagHelper, ICuddler
{
    public enum ECalloutType
    {
        Primary,
        Warning,
        Info,
        Danger
    }

    protected readonly HtmlEncoder HtmlEncoder;

    public CalloutTagHelper(HtmlEncoder htmlEncoder)
    {
        HtmlEncoder = htmlEncoder;
    }

    public string? Title { get; set; }

    public ECalloutType Type { get; set; } = ECalloutType.Primary;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        AddCalloutClasses(output);

        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(Title))
        {
            sb.Append("<h4>");
            sb.Append(Title);
            sb.Append("</h4>");
        }

        sb.Append(await GetInnerContent(output));

        output.Content.SetHtmlContent(new HtmlString(sb.ToString()));
    }

    private static async Task<string> GetInnerContent(TagHelperOutput output)
    {
        try
        {
            var innerHtml = (await output.GetChildContentAsync()).GetContent();
            if (!string.IsNullOrEmpty(innerHtml))
            {
                innerHtml = innerHtml.Trim();
            }

            return innerHtml;
        }
        catch (Exception ex)
        {
            throw new Exception("35d891fc-74e3-43b4-8704-9fd0479b3117", ex);
        }
    }

    private void AddCalloutClasses(TagHelperOutput output)
    {
        output.AddClass("bd-callout", HtmlEncoder);
        output.AddClass($"bd-callout-{Type.ToString().ToLower()}", HtmlEncoder);
    }
}