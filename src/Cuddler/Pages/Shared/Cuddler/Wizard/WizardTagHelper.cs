using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Forms.Utils;
using Cuddler.Ui;
using Cuddler.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Pages.Shared.Cuddler.Wizard;

public class WizardTagHelper : TagHelper, ICuddler
{
    protected readonly HtmlEncoder HtmlEncoder;

    protected readonly HtmlHelper HtmlHelper;

    private string? _entityId;
    private int _stepCount;
    private string? _stepUrl;

    public WizardTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
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

    public bool ShowSubmitButton { get; set; } = true;

    public bool EnableNavbar { get; set; } = true;

    public string? FormId { get; set; }

    public List<WizardStep> ListSteps { get; private set; } = new();

    public string? RedirectUrl { get; set; }

    public CuddlerUri? SubmitApiUrl { get; set; }

    public string SubmitButtonText { get; set; } = "Submit";

    public bool ShowNavigationButtons { get; set; } = true;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        (HtmlHelper as IViewContextAware).Contextualize(ViewContext);
        output.TagName = null;
        output.TagMode = TagMode.StartTagAndEndTag;

        var validationErrors = ValidateModelUtil.GetModelValidationErrors(this);
        if (validationErrors.Any())
        {
            foreach (var item in validationErrors)
            {
                throw new InvalidOperationException($"Invalid model item: {item.Key} = {item.Value}");
            }

            return;
        }

        var innerHtml = output.GetChildContentAsync().Result.GetContent().Trim();
        ListSteps = ParseSteps(innerHtml);
        
        var writer = new StringWriter();
        (await Partial()).WriteTo(writer, HtmlEncoder);
        output.Content.SetHtmlContent(writer.ToString());

        SetStep();
        SetEntityId();

        _stepUrl = ListSteps[_stepCount].StepUrl;

        if (!string.IsNullOrEmpty(_entityId))
        {
            _stepUrl = _stepUrl + "?id=" + _entityId;
        }

        // output.Content.SetHtmlContent(string.Empty);
    }

    public void SetHtml(IHtmlContent htmlString)
    {
        Html = htmlString;
    }

    private static async Task<string> GetInnerContent(TagHelperOutput output)
    {
        var content = await output.GetChildContentAsync();
        var innerHtml = content.GetContent();

        return innerHtml;
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

    public string? GetNextUrl()
    {
        var stepCount = GetStepCount() + 1;
        if (stepCount < ListSteps.Count)
        {
            var url = HtmlHelper.ViewContext.HttpContext.Request.CombinedPath() + $"?step={stepCount}";

            return url;
        }

        return null;
    }

    public int GetStepCount()
    {
        return _stepCount;
    }

    public string GetStepUrl()
    {
        return _stepUrl ?? throw new InvalidOperationException("0ea2c7a6-350f-4528-a801-5f178beb27e9");
    }

    private static object JsonDeserializeObject(Type type, string? json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return Activator.CreateInstance(type)!;
        }

        return JsonSerializer.Deserialize(json, type) ?? Activator.CreateInstance(type)!;
    }

    private static List<WizardStep> ParseSteps(string innerHtml)
    {
        var list = new List<WizardStep>();

        using StringReader reader = new(innerHtml);
        while (reader.ReadLine() is { } line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    line = line.Trim();
                    if (line.StartsWith("{") && line.EndsWith("}"))
                    {
                        var obj = (WizardStep)JsonDeserializeObject(typeof(WizardStep), line);
                        list.Add(obj);
                    }
                }
            }
        }

        return list;
    }

    private void SetEntityId()
    {
        HtmlHelper.ViewContext.HttpContext.Request.Query.TryGetValue("id", out var id);
        if (id.Any())
        {
            _entityId = id.ToString();
        }
    }

    private void SetStep()
    {
        HtmlHelper.ViewContext.HttpContext.Request.Query.TryGetValue("step", out var step);
        if (step.Any())
        {
            _stepCount = int.Parse(step.ToString());
        }
    }
}