using System.Text.Encodings.Web;

namespace Cuddler.Core.Utils;

public static class HtmlAttributesExtension
{
    public static void AddIfMissing(this IDictionary<string, object?> instance, string key, object? value)
    {
        if (!instance.ContainsKey(key) && value != null)
        {
            instance[key] = value;
        }
    }

    public static void AppendClass(this IDictionary<string, object?> dictionary, string parentStyles)
    {
        dictionary.AppendValue("class", " ", parentStyles);
    }

    public static void AppendStyles(this IDictionary<string, object?> dictionary, string parentStyles)
    {
        dictionary.AppendValue("style", " ", parentStyles);
    }

    public static void AppendValue(this IDictionary<string, object?> instance, string key, string separator, object value)
    {
        instance[key] = instance.ContainsKey(key)
            ? HtmlEncoder.Default.Encode(instance[key]!.ToString()!) + HtmlEncoder.Default.Encode(separator) + HtmlEncoder.Default.Encode(value.ToString()!)
            : HtmlEncoder.Default.Encode(value.ToString()!);
    }

    //public static IDictionary<string, object?> HtmlAttributes(this FormField model)
    //{
    //    var dictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(model.HtmlAttributes);
    //    if (model.ReadOnly)
    //    {
    //        AddIfMissing(dictionary, "readonly", "readonly");
    //        AppendValue(dictionary, "class", " ", "readonly");
    //    }

    //    if (!string.IsNullOrEmpty(model.Placeholder))
    //    {
    //        AddIfMissing(dictionary, "placeholder", model.Placeholder);
    //    }

    //    if (model.Required)
    //    {
    //        dictionary["required"] = "required";
    //        AddIfMissing(dictionary, "validationMessage", model.ErrorMessage);
    //    }

    //    if (model.MaxLength != null)
    //    {
    //        dictionary["maxlength"] = model.MaxLength;
    //    }

    //    return dictionary;
    //}
}