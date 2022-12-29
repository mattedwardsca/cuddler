﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.PopupEditor;

public class PopupEditorTagHelper : BaseTagHelper, ICuddler
{
    public PopupEditorTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        ButtonIcon = EFontAwesomeIcon.Window;

    }

    [Required]
    public EActionComplete ActionComplete { get; set; }

    [Required]
    public EFontAwesomeIcon ButtonIcon { get; set; }

    public string? ButtonText { get; set; }

    [Required]
    public EButtonType ButtonType { get; set; } = EButtonType.Primary;

    public string? CloseEvent { get; set; }

    public string? Id { get; set; }

    public bool Inline { get; set; }

    public string? PopupEvent { get; set; }

    public string? PopupHref { get; set; }

    public string? PopupTitle { get; set; }

    public string? RedirectUrl { get; set; }

    public string? ButtonWidth { get; set; }

    public CuddlerUri? SubmitApiUrl { get; set; }

    public bool SubmitButtonHide { get; set; }

    public string? SubmitButtonText { get; set; }

    [Required]
    public EButtonType SubmitButtonType { get; set; } = EButtonType.Success;

    public string? SubmitEvent { get; set; }
}