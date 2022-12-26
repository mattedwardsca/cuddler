using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.FormLabel;

public class FormLabelTagHelper : TagHelper
{
    protected readonly HtmlEncoder HtmlEncoder;
    protected readonly IHtmlHelper HtmlHelper;

    public FormLabelTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        HtmlHelper = htmlHelper;
        HtmlEncoder = htmlEncoder;
    }

    public bool Block { get; set; }

    public string? Description { get; set; }

    public string? ForProperty { get; set; }

    [HtmlAttributeNotBound]
    public IHtmlContent Html { get; set; } = null!;

    public string Id { get; set; } = null!;

    public bool Required { get; set; }

    public string? Text { get; set; }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Contextualize the html helper
        (HtmlHelper as IViewContextAware)!.Contextualize(ViewContext);

        await ConfigureContent(output);
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "div";
        output.AddClass("eux-FormLabel", HtmlEncoder.Default);
    }

    private async Task ConfigureContent(TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var innerHtml = content.GetContent();
        if (!string.IsNullOrEmpty(innerHtml))
        {
            innerHtml = innerHtml.Trim();
        }

        Html = new HtmlString(innerHtml);

        var html = (IHtmlContent)await HtmlHelper.PartialAsync("~/Pages/Shared/Cuddler/FormLabel/Default.cshtml", this);
        var writer = new StringWriter();
        html.WriteTo(writer, HtmlEncoder);
        output.Content.SetHtmlContent(writer.ToString());
    }
}