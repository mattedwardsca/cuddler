﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.ActionMenu;

public class ActionMenuTagHelper : BaseTagHelper, ICuddler
{
    public ActionMenuTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public string ButtonText { get; set; } = "Actions";

    [Required]
    public ActionMenuItems Menu { get; set; } = null!;
}