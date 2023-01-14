using System.Text;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.Paper;

public class PaperTagHelper : TagHelper, ICuddler
{
    public enum EDirection
    {
        Vertical,
        Horizontal
    }

    public enum EMargin
    {
        None,
        Preview,
        Hidden
    }

    protected readonly HtmlHelper HtmlHelper;

    public PaperTagHelper(IHtmlHelper htmlHelper)
    {
        HtmlHelper = htmlHelper as HtmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
    }

    public bool Border { get; set; } = true;

    public bool Height { get; set; }

    public EMargin Margin { get; set; } = EMargin.Hidden;

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        (HtmlHelper as IViewContextAware).Contextualize(ViewContext);

        output.AddClass("eux-Paper", HtmlEncoder.Default);

        if (Border)
        {
            output.AddClass("eux-Paper-border", HtmlEncoder.Default);
        }

        if (Height)
        {
            output.AddClass("eux-Paper-height", HtmlEncoder.Default);
        }

        if (Margin != EMargin.None)
        {
            var classNames = $"eux-Paper-margin eux-Paper-margin-{Margin}";

            var innerHtml = await GetInnerContent(output);
            var sb = new StringBuilder();
            sb.Append($"<div class=\"{classNames}\">");
            sb.Append(innerHtml);
            sb.Append("</div>");

            var html = new HtmlString(sb.ToString());
            output.Content.SetHtmlContent(html);
        }
    }

    private static async Task<string> GetInnerContent(TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        return InnerContent(content);
    }

    private static string InnerContent(TagHelperContent content)
    {
        try
        {
            var innerHtml = content.GetContent();
            if (!string.IsNullOrEmpty(innerHtml))
            {
                innerHtml = innerHtml.Trim();
            }

            return innerHtml;
        }
        catch (Exception e)
        {
            throw new Exception("ff68cb6e-5f4c-4634-bf1e-602d53c6fd0a", e);
        }
    }
}