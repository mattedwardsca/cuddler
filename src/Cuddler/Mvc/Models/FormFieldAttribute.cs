﻿namespace Cuddler.Mvc.Models;

[AttributeUsage(AttributeTargets.Property)]
public sealed class FormFieldAttribute : Attribute
{
    public FormFieldAttribute(string formField)
    {
        FormField = formField;
    }

    public FormFieldAttribute(string formField, bool hideLabel) : this(formField)
    {
        FormField = formField;
        HideLabel = hideLabel;
    }

    public string FormField { get; }

    public bool HideLabel { get; set; }
}