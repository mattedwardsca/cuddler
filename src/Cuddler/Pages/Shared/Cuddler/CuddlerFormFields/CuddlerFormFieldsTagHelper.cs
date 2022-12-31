﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Cuddler.Forms.BaseTagHelpers;
using Cuddler.Forms.Ui;
using Cuddler.Web.Api;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.CuddlerFormFields;

public class CuddlerFormFieldsTagHelper : BaseTagHelper, ICuddler
{
    public CuddlerFormFieldsTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder) : base(htmlHelper, htmlEncoder)
    {
    }

    public bool AutoSave { get; set; }

    [Required]
    public List<Forms.FormField> Fields { get; set; } = null!;

    public bool IsTemplate { get; set; }

    public bool IsView { get; set; }

    public CuddlerUri? SaveUrl { get; set; }
}