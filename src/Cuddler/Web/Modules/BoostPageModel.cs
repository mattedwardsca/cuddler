﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Web.Modules;

public class BoostPageModel : IMenuItem
{
    public BoostPageModel()
    {
    }

    public BoostPageModel(string? text)
    {
        Text = text;
    }

    public BoostPageModel(string? text, string description)
    {
        Text = text;
        Description = description;
    }

    public BoostPageModel(string? text, int count)
    {
        Text = text;
        Count = count;
    }

    //public string? Href { get; set; }
    public bool Selected { get; set; }

    public string? PageTitle { get; set; }

    public string? Description { get; set; }

    [NotMapped]
    public virtual List<IMenuItem>? Children { get; set; }

    public int? Count { get; set; }

    public bool Disabled { get; set; }

    public bool Hide { get; set; }

    public string? Icon { get; set; }

    public ELinkType LinkType { get; set; } = ELinkType.Link;

    public bool Locked { get; set; }

    public string? PageTemplateType { get; set; }

    public string? Query { get; set; }

    public string Segment => (string.IsNullOrEmpty(Text)
                                 ? string.Empty
                                 : AppMenuUtil.ConvertToPath(Text))
                             ?? string.Empty;

    [HiddenInput]
    public int SortOrder { get; set; }

    [Required]
    public string? Text { get; set; }
}