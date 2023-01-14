using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms;
using CuddlerDev.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Pages.Shared.Cuddler.FormField;

public class FormFieldTagHelper : TagHelper
{
    protected readonly HtmlEncoder HtmlEncoder;
    protected readonly IHtmlHelper HtmlHelper;
    private EFormField _template;

    public FormFieldTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
    {
        HtmlHelper = htmlHelper;
        HtmlEncoder = htmlEncoder;
    }

    // ReSharper disable once CollectionNeverUpdated.Global
    public IEnumerable<SelectListItem>? BindData { get; set; }

    public virtual string? BindId { get; set; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Forms.FormField>? Children { get; set; }

    public string? ContextType { get; set; }

    public string? Description { get; set; }

    public string? ErrorMessage { get; set; }

    /// <summary>
    /// String version of the Template
    /// </summary>
    public string? Field { get; set; }

    public bool HideLabel { get; set; }

    public object? HtmlAttributes { get; set; }

    public bool Inherited { get; set; }

    public bool IsTemplate { get; set; }

    public string? Label { get; set; }

    public bool Matrix { get; set; }

    public int? MaxLength { get; set; }
    
    public int? Tabindex { get; set; }

    public int? MinLength { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? CascadeFrom { get; set; }

    public string? Placeholder { get; set; }

    public bool ReadOnly { get; set; }

    public bool Required { get; set; }

    public bool ShowDescription { get; set; }

    public EFormField Template
    {
        get => _template;
        set
        {
            _template = value;
            Field = value.ToString();
        }
    }

    public string? Value { get; set; }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public string WebId { get; set; } = null!;

    /// <summary>
    ///     Do not use
    /// </summary>
    private Forms.FormField? FormField { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        (HtmlHelper as IViewContextAware)!.Contextualize(ViewContext);

        if (string.IsNullOrEmpty(Name))
        {
            throw new ArgumentException($"Name on input {Field} cannot be null. (Error: 49d4197b-6938-4f9a-b70b-4a6bbf559c7a)");
        }

        if (string.IsNullOrEmpty(WebId))
        {
            WebId = WebIdUtil.GetWebId();
        }

        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "div";
        if (ReadOnly)
        {
            output.AddClass("eux-readonly", HtmlEncoder.Default);
        }

        if (Field == EFormField.Hidden.ToString())
        {
            output.AddClass("eux-FormInput-hidden", HtmlEncoder.Default);
        }

        if (string.IsNullOrEmpty(Label))
        {
            Label = StringUtil.SplitCamelCase(Name);
        }

        await ConfigureContent(output);
    }

    public async Task<IHtmlContent> ToHtml()
    {
        try
        {
            if (FormField == null)
            {
                if (Field == null)
                {
                    return new HtmlString(string.Empty);
                }

                var formField = FormFieldUtil.GetFormField(WebId);
                ObjectCopierUtil.CopyProperties(this, formField);

                return await HtmlHelper.PartialAsync($"FormFields/{Field}", formField);
            }

            return await HtmlHelper.PartialAsync($"FormFields/{FormField.Field}", FormField);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured rendering {Field}. (Error: 7f458bb4-9782-4aab-ac3f-78f77b9936e8)", e);
        }
    }

    private async Task ConfigureContent(TagHelperOutput output)
    {
        var html = await HtmlHelper.PartialAsync("~/Pages/Shared/Cuddler/FormField/Default.cshtml", this);
        var writer = new StringWriter();
        html.WriteTo(writer, HtmlEncoder);
        output.Content.SetHtmlContent(writer.ToString());
    }
}