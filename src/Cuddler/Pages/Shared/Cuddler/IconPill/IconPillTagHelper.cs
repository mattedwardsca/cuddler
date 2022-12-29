﻿using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.IconPill;

public class IconPillTagHelper : BaseTagHelper, ICuddler
{
    public IconPillTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
        FontSize = EFontSize.OneX;
        IconPillType = EIconPillType.Info;
    }

    public EFontSize FontSize { get; set; }

    public string? Href { get; set; }

    public EFontAwesomeIcon Icon { get; set; }

    public EIconPillType IconPillType { get; set; }
}