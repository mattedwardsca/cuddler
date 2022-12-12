﻿using System.ComponentModel;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Cuddler.Internal._Utils;
using Cuddler.Mvc.Views;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler;

public static class CuddlerExtensions
{
    public static Cuddler Cuddler(this IHtmlHelper htmlHelper)
    {
        return new Cuddler(htmlHelper);
    }

    public static async Task<IHtmlContent> Template(this Cuddler cuddler, BaseTagHelper tagHelper)
    {
        var modelType = tagHelper.GetType();
        var tagHelperDictionary = ToObjectTagHelperDictionary(tagHelper);

        return await cuddler.Template(modelType, tagHelperDictionary);
    }

    public static async Task<IHtmlContent> Template(this Cuddler cuddler, Type modelType, IDictionary<string, object?> data)
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

        return await cuddler.HtmlHelper.PartialAsync($"Templates/{templateName}/Default", model);
    }

    public static async Task<IHtmlContent> Template(this Cuddler cuddler, string templateName, IDictionary<string, object?> data)
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

        return await cuddler.HtmlHelper.PartialAsync($"Templates/{templateName}/Default", model);
    }

    public static async Task<IHtmlContent> Template<TModel>(this Cuddler cuddler, Action<TModel>? f = null)
    {
        var type = typeof(TModel);
        f ??= _ => { };
        var model = (TModel)(Activator.CreateInstance(type, cuddler.HtmlHelper, HtmlEncoder.Default) ?? throw new InvalidOperationException());
        f.Invoke(model);
        var name = type.Name;
        name = name.Replace("Template", string.Empty);
        name = name.Replace("TagHelper", string.Empty);
        var result = await cuddler.HtmlHelper.PartialAsync($"Templates/{name}/Default", model);

        return result;
    }

    public static IDictionary<string, object?> ToTagHelperDictionary(this BaseTagHelper source)
    {
        return ToObjectTagHelperDictionary(source);
    }

    private static string? CalculatedDefaultValue(PropertyInfo propertyInfo)
    {
        var type = propertyInfo.PropertyType;

        object? obj = null;

        if (propertyInfo.ReflectedType != null && propertyInfo.ReflectedType.IsClass)
        {
            obj = Activator.CreateInstance(propertyInfo.ReflectedType);
        }
        else if (propertyInfo.DeclaringType != null && propertyInfo.DeclaringType.IsClass)
        {
            obj = Activator.CreateInstance(propertyInfo.DeclaringType);
        }

        if (obj != null)
        {
            var propertyValue = propertyInfo.GetValue(obj);

            return propertyValue?.ToString();
        }

        if (type == typeof(string) || type.IsClass)
        {
            return null;
        }

        if (type.IsEnum)
        {
            var instance = Activator.CreateInstance(type);

            return $"{type.Name}.{instance}";
        }

        var type1 = default(Type);

        return type1?.ToString();
    }

    private static string? GetDefaultValue(PropertyInfo propertyInfo)
    {
        var value = propertyInfo.GetCustomAttribute<DefaultValueAttribute>()
                                ?.Value;

        return value != null
            ? value.ToString()
            : CalculatedDefaultValue(propertyInfo);
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

    private static object? GetValue(object? obj, PropertyInfo propertyInfo)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        var methodInfo = propertyInfo.GetMethod;
        var result = methodInfo?.Invoke(obj, Array.Empty<object>());

        if (!propertyInfo.GetType()
                         .IsClass)
        {
            var stringValue = result?.ToString();
            if (string.IsNullOrEmpty(stringValue))
            {
                result = GetDefaultValue(propertyInfo);
            }
        }

        return result;
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
            var value = GetValue(source, propertyInfo);

            var property = GetKey(propertyInfo, null);
            if (!string.IsNullOrEmpty(property))
            {
                dictionary.Add(property, value);
            }
        }

        return dictionary;
    }
}