﻿namespace CuddlerDev.Forms.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class GridTemplateAttribute : Attribute
{
    public GridTemplateAttribute(EGridTemplate gridTemplate)
    {
        GridTemplate = gridTemplate.ToString();
    }

    public GridTemplateAttribute(string gridTemplate)
    {
        GridTemplate = gridTemplate;
    }

    public string GridTemplate { get; }
}