using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using CuddlerDev.Forms.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Forms.BaseTagHelpers;

public abstract class BaseTagHelper : TagHelper
{
    protected readonly HtmlEncoder HtmlEncoder = null!;

    protected readonly HtmlHelper HtmlHelper = null!;

    protected BaseTagHelper()
    {

    }

    protected BaseTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        HtmlHelper = htmlHelper as HtmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        HtmlEncoder = htmlEncoder;
    }

    public IHtmlContent Html { get; protected set; } = null!;

    [JsonIgnore]
    public bool ReadOnly { get; set; }

    [JsonIgnore]
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    protected string? Class { get; set; }

    [DebuggerStepThrough]
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        (HtmlHelper as IViewContextAware).Contextualize(ViewContext);

        await ConfigureContent(output);

        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = null;
    }

    public void SetHtml(IHtmlContent htmlString)
    {
        Html = htmlString;
    }

    private static async Task<string> GetInnerContent(TagHelperOutput output)
    {
        if (output == null)
        {
            throw new ArgumentNullException(nameof(output));
        }

        var content = await output.GetChildContentAsync();

        string innerHtml;
        try
        {
            innerHtml = content.GetContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return innerHtml;
    }

    [DebuggerStepThrough]
    private async Task ConfigureContent(TagHelperOutput output)
    {
        var innerHtml = await GetInnerContent(output);
        if (!string.IsNullOrEmpty(innerHtml))
        {
            innerHtml = innerHtml.Trim();
        }

        Html = new HtmlString(innerHtml);

        var validationErrors = ValidateModelUtil.GetModelValidationErrors(this);
        if (validationErrors.Any())
        {
            foreach (var item in validationErrors)
            {
                throw new InvalidOperationException($"Tag '{GetType().Name}' is called with in invalid property: '{item.Key}' : '{item.Value}'");
            }

            return;
        }

        var html = await Partial();

        var writer = new StringWriter();
        // inner content
        html.WriteTo(writer, HtmlEncoder);
        output.Content.SetHtmlContent(writer.ToString());
    }

    private async Task<IHtmlContent> Partial()
    {
        var t = GetType();
        var name = GetType()
            .Name;
        name = name.Replace("TagHelper", string.Empty);

        if (t.GetInterface(nameof(ICuddler)) != null)
        {
            return await HtmlHelper.PartialAsync($"Cuddler/{name}/Default", this);
        }

        if (t.GetInterface(nameof(ICuddlerButton)) != null)
        {
            return await HtmlHelper.PartialAsync($"Buttons/{name}/Default", this);
        }

        return await HtmlHelper.PartialAsync($"Templates/{name}/Default", this);
    }
}