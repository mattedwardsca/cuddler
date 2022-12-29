using System.Text.Encodings.Web;
using Cuddler.Data.Entities;
using Cuddler.Forms.Ui;
using Cuddler.Helpers;
using Cuddler.Pages.Shared.Cuddler.ArchiveButton;
using Cuddler.Pages.Shared.Cuddler.ButtonLink;
using Cuddler.Pages.Shared.Cuddler.CuddlerFormCreate;
using Cuddler.Pages.Shared.Cuddler.PopupEditor;
using Cuddler.Web.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.ActionMenu;

public class ActionMenuItems
{
    private readonly IHtmlHelper _htmlHelper;

    public ActionMenuItems(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper;
    }

    public List<IHtmlContent> MenuLinks { get; } = new();

    public async Task AddCuddlerCreate(Forms.CuddlerFormFields createModel, EActionComplete actionComplete = EActionComplete.Details, string? overrideButtonText = null, string? detailsUrl = null)
    {
        var tag = new CuddlerFormCreateTagHelper(_htmlHelper, HtmlEncoder.Default)
        {
            ActionComplete = actionComplete,
            CreateModel = createModel,
            DetailsUrl = detailsUrl,
            OverrideButtonText = overrideButtonText
        };

        var htmlContent = (await _htmlHelper.CuddlerUi()
                                            .Template(tag.GetType(), tag.ToTagHelperDictionary())).ToNullableString();

        MenuLinks.Add(new HtmlString(htmlContent));
    }

    public async Task AddPopupEditor(string text, string href, EFontAwesomeIcon icon = EFontAwesomeIcon.None)
    {
        var tag = new PopupEditorTagHelper(_htmlHelper, HtmlEncoder.Default)
        {
            ActionComplete = EActionComplete.Reload,
            ButtonIcon = icon,
            ButtonText = text,
            PopupTitle = text,
            ButtonType = EButtonType.ActionMenu,
            PopupHref = href,
            SubmitButtonHide = true
        };

        var htmlContent = (await _htmlHelper.CuddlerUi()
                                            .Template(tag.GetType(), tag.ToTagHelperDictionary())).ToNullableString();

        MenuLinks.Add(new HtmlString(htmlContent));
    }

    public async Task AddLink(string text, string href)
    {
        var tag = new ButtonLinkTagHelper(_htmlHelper, HtmlEncoder.Default)
        {
            ButtonType = EButtonType.Link,
            Href = href
        };
        tag.SetHtml(new HtmlString(text));

        var modelType = tag.GetType();
        var tagHelperDictionary = tag.ToTagHelperDictionary();

        var htmlContent = (await _htmlHelper.CuddlerUi()
                                            .Template(modelType, tagHelperDictionary)).ToNullableString();

        MenuLinks.Add(new HtmlString(htmlContent));
    }

    public async Task AddArchiveButton(IData data, string? overrideSingularText = null)
    {
        var tag = new ArchiveButtonTagHelper(_htmlHelper, HtmlEncoder.Default)
        {
            Data = data,
            ArchiveText = overrideSingularText,
            ButtonType = EArchiveButtonType.ActionMenu
        };

        var htmlContent = (await _htmlHelper.CuddlerUi()
                                            .Template(tag.GetType(), tag.ToTagHelperDictionary())).ToNullableString();

        MenuLinks.Add(new HtmlString(htmlContent));
    }

    public async Task AddDownloadLink(string text, string href)
    {
        var tag = new ButtonLinkTagHelper(_htmlHelper, HtmlEncoder.Default)
        {
            ButtonType = EButtonType.Link,
            Href = href,
            ButtonIcon = EFontAwesomeIcon.Download,
            Target = "_blank"
        };
        tag.SetHtml(new HtmlString(text));

        var htmlContent = (await _htmlHelper.CuddlerUi()
                                            .Template(tag.GetType(), tag.ToTagHelperDictionary())).ToNullableString();

        MenuLinks.Add(new HtmlString(htmlContent));
    }

    public async Task AddPartialAsync(string partialName, object obj)
    {
        var htmlContent = (await _htmlHelper.PartialAsync(partialName, obj)).ToNullableString();

        MenuLinks.Add(new HtmlString(htmlContent));
    }

    public async Task Add(IHtmlContent htmlContent)
    {
        MenuLinks.Add(htmlContent);

        await Task.CompletedTask;
    }
}