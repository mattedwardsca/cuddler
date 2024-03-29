﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Web.Models;

public class SelectListUrl : SelectListItem
{
    public SelectListUrl(string text, string value, string url) : base(text, value)
    {
        Url = url;
    }

    public string Url { get; set; }
}