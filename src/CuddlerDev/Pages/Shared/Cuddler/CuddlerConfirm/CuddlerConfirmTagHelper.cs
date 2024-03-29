﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerConfirm;

public class CuddlerConfirmTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerConfirmTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public EActionComplete ActionComplete { get; set; } = EActionComplete.Reload;

    [Required]
    public CuddlerUri ApiUrl { get; set; } = null!;

    [Required]
    public EFontAwesomeIcon ButtonIcon { get; set; } = EFontAwesomeIcon.Edit;

    public string? ButtonText { get; set; }

    [Required]
    public EButtonType ButtonType { get; set; } = EButtonType.Primary;
}