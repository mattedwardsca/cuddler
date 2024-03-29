﻿using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.Divider;

public class DividerTagHelper : BaseTagHelper, ICuddler
{
    public DividerTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {

        Height = 2;
        Color = "var(--ColorFive)";
    }

    public string Color { get; set; }

    public int Height { get; set; }

    public int? Width { get; set; }
}