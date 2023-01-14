using System.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace CuddlerDev.Pages.Shared.Cuddler.Translate;

public class TranslateTagHelper : TagHelper, ICuddler
{
    private readonly IStringLocalizer<TranslateTagHelper> _localizer;

    public TranslateTagHelper(IStringLocalizer<TranslateTagHelper> localizer)
    {
        _localizer = localizer;
    }

    /// <summary>
    ///     Show button for editing translation
    ///     TODO: this will need to be enabled globally
    /// </summary>
    public bool Display { get; set; }

    public bool HideText { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        string innerHtml;
        try
        {
            innerHtml = output.GetChildContentAsync()
                              .Result.GetContent();
        }
        catch (Exception ex)
        {
            throw new Exception(nameof(TranslateTagHelper), ex);
        }

        if (!string.IsNullOrEmpty(innerHtml))
        {
            innerHtml = innerHtml.Trim();
            innerHtml = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(innerHtml));
        }

        output.TagName = "span";

        if (!string.IsNullOrEmpty(innerHtml))
        {
            var html = innerHtml;
            var value = HttpUtility.HtmlDecode(_localizer[html]);
            if (Display)
            {
                var translateUi = TranslateUi(innerHtml);
                if (HideText)
                {
                    output.Content.SetHtmlContent(translateUi);
                }
                else
                {
                    output.Content.SetHtmlContent(translateUi + value);
                }
            }
            else
            {
                if (!HideText)
                {
                    output.Content.SetHtmlContent(value);
                }
                else
                {
                    output.Content.SetContent(string.Empty);
                }
            }
        }
        else
        {
            output.Content.SetContent(string.Empty);
        }

        await Task.CompletedTask;
    }

    private static string TranslateUi(string key)
    {
        key = HttpUtility.UrlEncode(key);

        return $"<span class=\"eux-lang-missing fas fa-globe fa-xs me-1\" onclick=\"editTranslationKey(event, '{key}'); return false;\"></span>";
    }
}