﻿using System.ComponentModel;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.ShortcutButton;

public class ShortcutButtonTagHelper : BaseTagHelper, ICuddler
{
    public ShortcutButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string? Href { get; set; }

    public EFontAwesomeIcon Icon { get; set; }

    public int Number { get; set; }

    [Description("When true, and the override number is greater than 0, the icon is replaced with the number.")]
    public bool OverrideIcon { get; set; }

    public EShortcutButtonSize Size { get; set; } = EShortcutButtonSize.Regular;

    public string? Text { get; set; }
}