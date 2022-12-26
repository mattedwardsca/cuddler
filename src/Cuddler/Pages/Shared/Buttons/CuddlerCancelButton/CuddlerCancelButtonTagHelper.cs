﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Pages.Shared.Cuddler.Base;
using Cuddler.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Buttons.CuddlerCancelButton;

public class CuddlerCancelButtonTagHelper : BaseTagHelper, ICuddlerButton
{
    public CuddlerCancelButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public CuddlerUri CancelUri { get; set; } = null!;

    [Required]
    public string Label { get; set; } = "Cancel";

    [Required]
    public string RedirectUrl { get; set; } = null!;
}