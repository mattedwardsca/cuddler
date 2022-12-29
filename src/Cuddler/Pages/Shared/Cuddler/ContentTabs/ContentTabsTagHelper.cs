using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cuddler.Core.Utils;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Forms.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.ContentTabs;

[RestrictChildren("tab")]
public class ContentTabsTagHelper : TagHelper, ICuddler
{
    protected readonly HtmlEncoder HtmlEncoder;

    protected readonly HtmlHelper HtmlHelper;

    protected ETagType TagType;

    public ContentTabsTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        HtmlHelper = htmlHelper as HtmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        HtmlEncoder = htmlEncoder;
    }

    public IHtmlContent Html { get; protected set; } = null!;

    [JsonIgnore]
    public bool ReadOnly { get; set; }

    public List<TabTagHelper> TabList { get; private set; } = null!;

    public bool TrackHistory { get; set; } = true;

    [JsonIgnore]
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

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

        var innerHtml = (await output.GetChildContentAsync()).GetContent()
                                                             .Trim();
        TabList = ParseInnerContent(innerHtml);

        var writer = new StringWriter();
        (await Partial()).WriteTo(writer, HtmlEncoder);
        output.Content.SetHtmlContent(writer.ToString());
        //output.Content.SetHtmlContent(string.Empty);
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

    private static List<TabTagHelper> ParseInnerContent(string innerHtml)
    {
        var list = new List<TabTagHelper>();

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

            var obj = (TabTagHelper)JsonDeserializeObject(typeof(TabTagHelper), line);
            list.Add(obj);
        }

        return list;
    }

    private async Task<IHtmlContent> Partial()
    {
        var name = GetType()
            .Name;
        name = name.Replace("TagHelper", string.Empty);

        return await HtmlHelper.PartialAsync($"Templates/{name}/Default", this);
    }
}