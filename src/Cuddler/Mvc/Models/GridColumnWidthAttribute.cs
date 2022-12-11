﻿namespace Cuddler.Mvc.Models;

[AttributeUsage(AttributeTargets.Property)]
public sealed class GridColumnWidthAttribute : Attribute
{
    public GridColumnWidthAttribute(int w)
    {
        Width = w;
    }

    public int Width { get; set; }
}