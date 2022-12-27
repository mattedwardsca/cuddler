using System.Text;
using System.Text.Encodings.Web;
using Cuddler.Web.BaseTagHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.Breadcrumbs;

public class BreadcrumbsTagHelper : TagHelper, ICuddler
{
    protected readonly HtmlHelper HtmlHelper;

    public BreadcrumbsTagHelper(IHtmlHelper htmlHelper)
    {
        HtmlHelper = htmlHelper as HtmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        (HtmlHelper as IViewContextAware).Contextualize(ViewContext);

        output.TagName = null;
        var innerHtml = await GetInnerContent(output);
        if (!string.IsNullOrEmpty(innerHtml))
        {
            innerHtml = innerHtml.Trim();
        }

        var sb = new StringBuilder();
        sb.Append(innerHtml);
        var html = new HtmlString(sb.ToString());

        var writer = new StringWriter();
        html.WriteTo(writer, HtmlEncoder.Default);
        output.Content.SetHtmlContent(string.Empty);

        HtmlHelper.ViewData["Breadcrumbs"] = writer.ToString();
    }

    private static async Task<string> GetInnerContent(TagHelperOutput output)
    {
        try
        {
            var content = await output.GetChildContentAsync();
            var innerHtml = content.GetContent();

            return innerHtml;
        }
        catch (Exception ex)
        {
            throw new Exception("77cc447b-0cd1-4ced-84fe-6cbc747a7a04", ex);
        }
    }
}