﻿using System.Text;

namespace Cuddler.Mvc.Models;

[AttributeUsage(AttributeTargets.Property)]
public class RowAttribute : Attribute
{
    public RowAttribute()
    {
    }

    public RowAttribute(int rowStart, int rowSpan)
    {
        RowStart = rowStart;
        RowSpan = rowSpan;
    }

    public int RowSpan { get; set; }

    public int RowStart { get; set; }

    public string ToLayoutStyles()
    {
        var sb = new StringBuilder();

        // grid-row: <start-line> / <end-line> | <start-line> / span <value>;
        sb.Append("grid-row: " + RowStart + " / span " + RowSpan + ";");

        return sb.ToString();
    }
}