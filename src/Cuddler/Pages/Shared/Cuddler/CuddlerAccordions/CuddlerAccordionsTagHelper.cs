using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Forms.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerAccordions;

[RestrictChildren("cuddler-accordion")]
public class CuddlerAccordionsTagHelper : TagHelper, ICuddler
{
    protected readonly HtmlEncoder HtmlEncoder;

    protected readonly HtmlHelper HtmlHelper;

    protected ETagType TagType;

    public CuddlerAccordionsTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        HtmlHelper = htmlHelper as HtmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        HtmlEncoder = htmlEncoder;
    }

    public IHtmlContent Html { get; protected set; } = null!;

    [JsonIgnore]
    public bool ReadOnly { get; set; }

    public List<CuddlerAccordionTagHelperModel> TabList { get; private set; } = null!;

    [JsonIgnore]
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    protected string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        (HtmlHelper as IViewContextAware).Contextualize(ViewContext);
        output.TagName = null;

        var validationErrors = ValidateModelUtil.GetModelValidationErrors(this);
        if (validationErrors.Any())
        {
            foreach (var item in validationErrors)
            {
                throw new InvalidOperationException($"Invalid model item: {item.Key} = {item.Value}");
            }

            return;
        }

        TabList = ParseInnerContent(output.GetChildContentAsync()
                                          .Result.GetContent()
                                          .Trim());

        var html = await Partial();
        var writer = new StringWriter();
        html.WriteTo(writer, HtmlEncoder);
        output.Content.SetHtmlContent(writer.ToString());
    }

    public void SetHtml(IHtmlContent htmlString)
    {
        Html = htmlString;
    }

    private static object JsonDeserializeObject(Type type, string? json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return Activator.CreateInstance(type)!;
        }

        return JsonSerializer.Deserialize(json, type) ?? Activator.CreateInstance(type)!;
    }

    private static List<CuddlerAccordionTagHelperModel> ParseInnerContent(string innerHtml)
    {
        var list = new List<CuddlerAccordionTagHelperModel>();

        using StringReader reader = new(innerHtml);
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(line))
            {
                continue;
            }

            line = line.Trim();
            if (!line.StartsWith("{") || !line.EndsWith("}"))
            {
                continue;
            }

            var obj = (CuddlerAccordionTagHelperModel)JsonDeserializeObject(typeof(CuddlerAccordionTagHelperModel), line);
            list.Add(obj);
        }

        return list;
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

        return await HtmlHelper.PartialAsync($"Templates/{name}/Default", this);
    }
}