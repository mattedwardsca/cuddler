﻿using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.Today;

public class TodayTagHelper : BaseTagHelper, ICuddler
{
    public TodayTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string? Label { get; set; }

    public bool Required { get; set; }

    public bool ShowDaterange { get; set; }

    public bool ShowMonthbutton { get; set; }

    public bool ShowTodaybutton { get; set; }

    public bool ShowWeekbutton { get; set; }
}