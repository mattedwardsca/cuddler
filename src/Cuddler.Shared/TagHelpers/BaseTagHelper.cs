using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Shared.TagHelpers;

public abstract class BaseTagHelper : TagHelper
{
    protected readonly HtmlEncoder HtmlEncoder;

    protected readonly HtmlHelper HtmlHelper;

    protected ETagType TagType;

    protected BaseTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        HtmlHelper = htmlHelper as HtmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        HtmlEncoder = htmlEncoder;
        TagType = ETagType.Div;
    }

    public IHtmlContent Html { get; protected set; } = null!;

    [JsonIgnore]
    public bool ReadOnly { get; set; }

    [JsonIgnore]
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    protected string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        (HtmlHelper as IViewContextAware).Contextualize(ViewContext);

        await ConfigureContent(output);
        ConfigureTag(output);
    }

    public void SetHtml(IHtmlContent htmlString)
    {
        Html = htmlString;
    }

    protected abstract Task ProcessWidgetAsync(TagHelperOutput output);

    private static async Task<string> GetInnerContent(TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var innerHtml = content.GetContent();

        return innerHtml;
    }

    private void AddCssClasses(TagHelperOutput output)
    {
        output.AddClass("eux-Component", HtmlEncoder);
        output.AddClass($"{GetClassName()}", HtmlEncoder);
        if (!string.IsNullOrEmpty(Class))
        {
            var classValues = Class.Split(" ")
                                   .Where(c => !string.IsNullOrEmpty(c));

            foreach (var classValue in classValues)
            {
                output.AddClass(classValue, HtmlEncoder);
            }
        }
    }

    private async Task ConfigureContent(TagHelperOutput output)
    {
        var innerHtml = await GetInnerContent(output);
        if (!string.IsNullOrEmpty(innerHtml))
        {
            innerHtml = innerHtml.Trim();
        }

        Html = new HtmlString(innerHtml);

        await ProcessWidgetAsync(output);
        var html = await Partial();

        var writer = new StringWriter();
        // inner content
        html.WriteTo(writer, HtmlEncoder);
        output.Content.SetHtmlContent(writer.ToString());
    }

    private void ConfigureTag(TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        switch (TagType)
        {
            case ETagType.Article:
                AddCssClasses(output);
                output.TagName = "article";

                break;

            case ETagType.Div:
                AddCssClasses(output);
                output.TagName = "div";

                break;

            case ETagType.Nav:
                AddCssClasses(output);
                output.TagName = "nav";

                break;

            case ETagType.Span:
                AddCssClasses(output);
                output.TagName = "span";

                break;

            case ETagType.Head:
                output.TagName = "head";

                break;

            case ETagType.Body:
                output.TagName = "body";

                break;

            case ETagType.Li:
                AddCssClasses(output);
                output.TagName = "li";

                break;

            case ETagType.Link:
                AddCssClasses(output);
                output.TagName = "a";

                break;

            case ETagType.None:
                output.TagName = null;

                break;

            case ETagType.Button:
                AddCssClasses(output);
                output.TagName = "button";
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private string GetClassName()
    {
        var name = GetType()
            .Name;

        name = name.Replace("TagHelper", string.Empty);

        return $"eux-{name}";
    }

    private async Task<IHtmlContent> Partial()
    {
        var name = GetType()
            .Name;
        name = name.Replace("TagHelper", string.Empty);
        return await HtmlHelper.PartialAsync($"Templates/{name}/Default", this);
    }
}