﻿using System.Text.Encodings.Web;
using CuddlerDev.Data.Entities;
using CuddlerDev.Pages.Shared.Cuddler.ArchiveButton;
using CuddlerDev.Pages.Shared.Cuddler.CuddlerLink;
using CuddlerDev.Pages.Shared.Cuddler.PopupEditor;
using CuddlerDev.Ui;
using CuddlerDev.Utils;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.ActionMenu;

public class ActionMenuItems
{
    private readonly IHtmlHelper _htmlHelper;

    public ActionMenuItems(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper;
    }

    public List<IHtmlContent> MenuLinks { get; } = new();

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
        var tag = new CuddlerLinkTagHelper(_htmlHelper, HtmlEncoder.Default)
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

    public async Task AddArchiveButton(IData data)
    {
        var tag = new ArchiveButtonTagHelper(_htmlHelper, HtmlEncoder.Default)
        {
            Data = data,
            ButtonType = EArchiveButtonType.ActionMenu
        };

        var htmlContent = (await _htmlHelper.CuddlerUi()
                                            .Template(tag.GetType(), tag.ToTagHelperDictionary())).ToNullableString();

        MenuLinks.Add(new HtmlString(htmlContent));
    }

    public async Task AddDownloadLink(string text, string href)
    {
        var tag = new CuddlerLinkTagHelper(_htmlHelper, HtmlEncoder.Default)
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