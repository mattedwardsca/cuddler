using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using CuddlerDev.Forms;
using CuddlerDev.Forms.Attributes;
using CuddlerDev.Forms.BaseTagHelpers;
using CuddlerDev.Forms.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CuddlerDev.Ui;

public static class CuddlerUiTemplateExtensions
{

    public static async Task<IHtmlContent> Template(this CuddlerUi cuddler, BaseTagHelper tagHelper)
    {
        var modelType = tagHelper.GetType();
        var tagHelperDictionary = ToObjectTagHelperDictionary(tagHelper);

        return await cuddler.Template(modelType, tagHelperDictionary);
    }

    public static async Task<IHtmlContent> Template(this CuddlerUi cuddler, Type modelType, IDictionary<string, object?> data)
    {
        object? model;
        try
        {
            model = Activator.CreateInstance(modelType, cuddler.HtmlHelper, HtmlEncoder.Default);
        }
        catch (Exception)
        {
            return new HtmlString("Unable to create instance of template: " + modelType.Name);
        }

        UpdateModelUtil.UpdateModelValues(model, data);
        var templateName = modelType.Name.Replace("TagHelper", string.Empty);

        if (modelType.GetInterface(nameof(ICuddler)) != null)
        {
            return await cuddler.HtmlHelper.PartialAsync($"Cuddler/{templateName}/Default", model);
        }

        return await cuddler.HtmlHelper.PartialAsync($"Templates/{templateName}/Default", model);
    }

    public static async Task<IHtmlContent> Template(this CuddlerUi cuddler, string templateName, IDictionary<string, object?> data)
    {
        object? model;
        Type? modelType;
        try
        {
            modelType = Type.GetType(templateName);
            model = Activator.CreateInstance(modelType ?? throw new InvalidOperationException());
        }
        catch (Exception)
        {
            return new HtmlString("Invalid template: " + templateName);
        }

        UpdateModelUtil.UpdateModelValues(model, data);

        templateName = modelType.Name.Replace("TagHelper", string.Empty);

        if (modelType.GetInterface(nameof(ICuddler)) != null)
        {
            return await cuddler.HtmlHelper.PartialAsync($"Cuddler/{templateName}/Default", model);
        }

        return await cuddler.HtmlHelper.PartialAsync($"Templates/{templateName}/Default", model);
    }

    public static async Task<IHtmlContent> Template<TModel>(this CuddlerUi cuddler, Action<TModel>? f = null)
    {
        var type = typeof(TModel);
        f ??= _ => {
        };
        var model = (TModel)(Activator.CreateInstance(type, cuddler.HtmlHelper, HtmlEncoder.Default) ?? throw new InvalidOperationException());
        f.Invoke(model);
        var name = type.Name;

        name = name.Replace("TagHelper", string.Empty);

        if (type.GetInterface(nameof(ICuddler)) != null)
        {
            return await cuddler.HtmlHelper.PartialAsync($"Cuddler/{name}/Default", model);
        }

        var result = await cuddler.HtmlHelper.PartialAsync($"Templates/{name}/Default", model);

        return result;
    }

    public static IDictionary<string, object?> ToTagHelperDictionary(this object source)
    {
        return ToObjectTagHelperDictionary(source);
    }

    private static string? GetKey(PropertyInfo propertyInfo, string? propertyInfoParentName)
    {
        var hasAttribute = HasAttribute<HtmlAttributeNotBoundAttribute>(propertyInfo);
        if (hasAttribute)
        {
            return null;
        }

        var propertyInfoName = propertyInfo.Name;
        if (!string.IsNullOrEmpty(propertyInfoParentName))
        {
            propertyInfoName = propertyInfoParentName + "." + propertyInfoName;
        }

        var ignore = HasAttribute<JsonIgnoreAttribute>(propertyInfo);
        if (ignore)
        {
            return null;
        }

        var isVirtual = GetVirtual(propertyInfo);
        if (isVirtual)
        {
            return null;
        }

        var isInherited = HasAttribute<InheritedInputAttribute>(propertyInfo);
        if (isInherited)
        {
            return null;
        }

        return propertyInfoName;
    }

    private static bool GetVirtual(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetMethod!.IsVirtual;
    }

    private static bool HasAttribute<T>(PropertyInfo propertyInfo)
    {
        return Attribute.IsDefined(propertyInfo, typeof(T));
    }

    private static IDictionary<string, object?> ToObjectTagHelperDictionary(object source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source), "Unable to convert object to a dictionary. The source object is null.");
        }

        var dictionary = new Dictionary<string, object?>();
        var t = source.GetType();
        var properties = t.GetProperties();
        foreach (var propertyInfo in properties)
        {
            var value = FormFieldUtil.GetValue(source, propertyInfo);

            var property = GetKey(propertyInfo, null);
            if (!string.IsNullOrEmpty(property))
            {
                dictionary.Add(property, value);
            }
        }

        return dictionary;
    }
}