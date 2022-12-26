using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Encodings.Web;
using Cuddler.Core.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Cuddler.Core.Forms;

public class FormField
{
    private string? _errorMessage;
    private object? _htmlAttributes;
    private string? _value;

    public FormField()
    {
    }

    public FormField(string id)
    {
        WebId = id;
    }

    [NotMapped]
    public virtual PropertyInfo BackingProperty { get; set; } = null!;

    public IEnumerable<SelectListItem>? BindData { get; set; }

    [NotMapped]
    public virtual string? BindId { get; set; }

    public string? CascadeFrom { get; set; }

    public virtual List<FormField>? Children { get; set; }

    public int Col { get; set; }

    public int? ColLength { get; set; }

    [NotMapped]
    public string? ContextType { get; set; }

    public string? DataType { get; set; }

    /// <summary>
    ///     For public use
    /// </summary>
    [NotMapped]
    public object? DefaultValue { get; set; }

    [NotMapped]
    public string? Description { get; set; }

    public string? ErrorMessage
    {
        get => _errorMessage ?? $"{StringUtil.SplitCamelCase(Name)} is required";
        set => _errorMessage = value;
    }

    public string? Field { get; set; }

    public Type? ForeignKey { get; set; }

    /// <summary>
    ///     Width of a grid column
    /// </summary>
    public int? GridColumnWidth { get; set; }

    public string? GridTemplate { get; set; }

    [NotMapped]
    public object? HeaderHtmlAttributes { get; set; }

    public bool HideLabel { get; set; }

    [NotMapped]
    public object? HtmlAttributes
    {
        get
        {
            var dictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(_htmlAttributes);
            if (ReadOnly)
            {
                AddIfMissing(dictionary, "readonly", "readonly");
                AppendValue(dictionary, "class", " ", "readonly");
            }

            if (!string.IsNullOrEmpty(Placeholder))
            {
                AddIfMissing(dictionary, "placeholder", Placeholder);
            }

            if (Required)
            {
                dictionary["required"] = "required";
                AddIfMissing(dictionary, "validationMessage", ErrorMessage);
            }

            if (MaxLength != null)
            {
                dictionary["maxlength"] = MaxLength;
            }

            return dictionary;
        }
        set => _htmlAttributes = value;
    }

    /// <summary>
    ///     For public use
    /// </summary>
    [NotMapped]
    public bool Inherited { get; set; }

    [NotMapped]
    public bool IsTemplate { get; set; }

    public string? Label { get; set; }

    public bool Matrix { get; set; }

    public int? MaxLength { get; set; }

    public int? MinLength { get; set; }

    public string? MoreInfo { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    //[NotMapped]
    //public EBlurAction OnBlur { get; set; } = EBlurAction.None;

    [NotMapped]
    public string? Placeholder { get; set; }

    [NotMapped]
    public bool ReadOnly { get; set; }

    [NotMapped]
    public bool Required { get; set; }

    /// <summary>
    ///     For public use
    /// </summary>
    [NotMapped]
    public string[]? Roles { get; set; }

    public int Row { get; set; }

    public string? Tag { get; set; }

    //public EFormField Template
    //{
    //    set => Field = value.ToString();
    //}

    [NotMapped]
    public string? Value
    {
        get =>
            IsTemplate
                ? $"#= {Name} ?? \'\' #"
                : _value;
        set => _value = value;
    }

    /// <summary>
    ///     For public use
    /// </summary>
    [NotMapped]
    public bool Virtual { get; set; }

    public string WebId { get; set; } = null!;

    /// <summary>
    ///     For public use
    /// </summary>
    public virtual Type GetDataType()
    {
        return DataType switch
        {
            nameof(String) => typeof(string),
            nameof(Int32) => typeof(int),
            nameof(Decimal) => typeof(decimal),
            nameof(Double) => typeof(double),
            nameof(DateTime) => typeof(DateTime),
            _ => typeof(string)
        };

        //throw new InvalidOperationException($"{DataType} (Error: 70a81752-f302-4804-b7ae-f76df2613ae4)");
    }

    public void SetDataType(Type type)
    {
        if (type == typeof(string))
        {
            DataType = nameof(String);
        }
        else if (type == typeof(int))
        {
            DataType = nameof(Int32);
        }
        else if (type == typeof(decimal))
        {
            DataType = nameof(Decimal);
        }
        else if (type == typeof(double))
        {
            DataType = nameof(Double);
        }
        else if (type == typeof(DateTime))
        {
            DataType = nameof(DateTime);
        }
        else if (type == typeof(float))
        {
            DataType = nameof(Double);
        }

        throw new InvalidOperationException($"{type.Name} (Error: a99496c6-994a-49f4-b342-46d8e286688f)");
    }

    public override string ToString()
    {
        return $"{Name} ({Field})";
    }

    private static void AddIfMissing(IDictionary<string, object?> instance, string key, object? value)
    {
        if (!instance.ContainsKey(key) && value != null)
        {
            instance[key] = value;
        }
    }

    private static void AppendValue(IDictionary<string, object?> instance, string key, string separator, object value)
    {
        instance[key] = instance.ContainsKey(key)
            ? HtmlEncoder.Default.Encode(instance[key]!.ToString()!) + HtmlEncoder.Default.Encode(separator) + HtmlEncoder.Default.Encode(value.ToString()!)
            : HtmlEncoder.Default.Encode(value.ToString()!);
    }
}