using System.Text;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerToolbar;

public class CuddlerToolbarTagHelper : TagHelper, ICuddler
{
    protected readonly HtmlHelper HtmlHelper;

    public CuddlerToolbarTagHelper(IHtmlHelper htmlHelper)
    {
        HtmlHelper = htmlHelper as HtmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
    }

    public bool SpaceBetween { get; set; }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await Task.CompletedTask;

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

        HtmlHelper.ViewData["Toolbar"] = writer.ToString();
        HtmlHelper.ViewData["Toolbar_Justify"] = SpaceBetween;
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
            throw new Exception("e284f90e-6864-47a7-8c0f-065d523906d2", ex);
        }
    }
}