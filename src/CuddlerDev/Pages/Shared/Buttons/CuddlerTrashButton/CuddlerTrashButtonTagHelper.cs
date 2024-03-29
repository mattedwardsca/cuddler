﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Ui;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Buttons.CuddlerTrashButton;

public class CuddlerTrashButtonTagHelper : BaseTagHelper, ICuddlerButton
{
    public CuddlerTrashButtonTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    [Required]
    public CuddlerUri ArchiveUri { get; set; } = null!;

    public bool CantBeUndone { get; set; }

    [Required]
    public string Label { get; set; } = "Remove item";
}