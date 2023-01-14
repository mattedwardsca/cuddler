using CuddlerDev.Forms.Helpers;

namespace CuddlerDev.Forms.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class FormFieldAttribute : Attribute
{
    public FormFieldAttribute(string formField)
    {
        FormField = formField;
    }

    public FormFieldAttribute(EFormField formField, ELookupCategory lookupCategory)
    {
        FormField = formField.ToString();
        ContextType = lookupCategory.ToString();
    }

    public FormFieldAttribute(string formField, bool hideLabel) : this(formField)
    {
        FormField = formField;
        HideLabel = hideLabel;
    }

    public FormFieldAttribute(EFormField formField)
    {
        FormField = formField.ToString();
    }

    public FormFieldAttribute(EFormField formField, bool hideLabel) : this(formField)
    {
        HideLabel = hideLabel;
    }

    public string FormField { get; }

    public string? ContextType { get; }

    public bool HideLabel { get; set; }
}