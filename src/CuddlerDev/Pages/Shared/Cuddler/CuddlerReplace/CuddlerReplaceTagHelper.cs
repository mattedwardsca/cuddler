﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Web.Blocks;
using CuddlerDev.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Pages.Shared.Cuddler.CuddlerReplace;

public class CuddlerReplaceTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerReplaceTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public EDynamicHandler Handler { get; set; }

    [Required]
    public string Url { get; set; } = null!;

    [Required]
    public string Label { get; set; } = "Edit";

    public EFontAwesomeIcon Icon { get; set; } = EFontAwesomeIcon.Edit;

    public string BlockId { get; set; } = null!;

    public string GetUrlHandler()
    {
        return $"{Url}?handler={Handler.ToString()}";
    }
}