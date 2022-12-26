using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using Cuddler.Core.Attributes;
using Cuddler.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cuddler.Core.Forms;

public static class FormFieldUtil
{
    public static string? ClientTemplate(string key, string kendoGridTemplate)
    {
        return KeyTemplate(key, kendoGridTemplate);
    }

    public static FormField GetDefaultFormField(string key, object? value)
    {
        return new FormField
        {
            Name = key,
            Label = key,
            DataType = "String",
            Field = "String",
            DefaultValue = value,
            GridTemplate = ClientTemplate(key, nameof(EGridTemplate.Text))
        };
    }

    public static FormField GetFormField(string id)
    {
        return new FormField(id);
    }

    public static FormField? GetFormField(object? obj, PropertyInfo propertyInfo, string childSeparator = ".")
    {
        var value = GetValue(obj, propertyInfo);

        return GetFormField(propertyInfo, null, value, childSeparator);
    }

    public static FormField? GetFormField(Type t, string nameOfProperty)
    {
        var propertyInfo = GetPropertyInfo(t, nameOfProperty);
        var instanceofType = Activator.CreateInstance(t);
        return propertyInfo == null
            ? null
            : GetFormField(instanceofType, propertyInfo);
    }

    public static FormField? GetFormField(PropertyInfo propertyInfo, string? propertyInfoParentName, object? value, string childSeparator)
    {
        var propertyInfoName = propertyInfo.Name;
        if (!string.IsNullOrEmpty(propertyInfoParentName))
        {
            propertyInfoName = propertyInfoParentName + childSeparator + propertyInfoName;
        }

        var ignore = HasAttribute<JsonIgnoreAttribute>(propertyInfo);
        if (ignore)
        {
            return null;
        }

        var isClass = IsClass(propertyInfo);

        var property = new FormField
        {
            BackingProperty = propertyInfo,
            BindData = GetBindData(propertyInfo),
            Children = isClass
                ? ListChildren(propertyInfo, propertyInfoName, value, childSeparator)
                : null,
            DataType = GetDataType(propertyInfo),
            DefaultValue = GetDefaultValue(propertyInfo),
            Description = GetDescription(propertyInfo),
            ErrorMessage = GetErrorMessage(propertyInfo),
            Field = GetInputTemplate(propertyInfo),
            GridColumnWidth = GetColumnWidth(propertyInfo),
            GridTemplate = GetGridTemplate(propertyInfo, propertyInfoName),
            HideLabel = GetHideLabel(propertyInfo),
            Inherited = HasAttribute<InheritedInputAttribute>(propertyInfo),
            Label = GetLabel(propertyInfo),
            CascadeFrom = GetCascadeFrom(propertyInfo),
            Name = propertyInfoName,
            Placeholder = GetPlaceholder(propertyInfo),
            ReadOnly = GetReadOnly(propertyInfo),
            Required = GetRequired(propertyInfo),
            Tag = GetFormTag(propertyInfo),
            Value = isClass
                ? null
                : value?.ToString(),
            Virtual = GetVirtual(propertyInfo)
        };

        GetLayout(property, propertyInfo);

        return property;
    }

    public static string? GetGridTemplate(PropertyInfo propertyInfo, string key)
    {
        var gridTemplate = propertyInfo.GetCustomAttribute<GridTemplateAttribute>();
        if (gridTemplate != null && !string.IsNullOrEmpty(gridTemplate.GridTemplate))
        {
            var clientTemplate = ClientTemplate(key, gridTemplate.GridTemplate);

            if (clientTemplate != null)
            {
                return clientTemplate;
            }

            return $"# if(typeof {gridTemplate.GridTemplate} === 'function'){{# #={gridTemplate.GridTemplate}({key})# #}} else {{ console.log('Missing grid template function: \\'{gridTemplate.GridTemplate}\\'') }}#";
        }

        var dataType = GetDataType(propertyInfo);

        if (dataType == nameof(String))
        {
            return ClientTemplate(key, nameof(EGridTemplate.Text));
        }

        if (dataType == nameof(Int32))
        {
            return ClientTemplate(key, nameof(EGridTemplate.Integer));
        }

        if (dataType is nameof(Decimal) or nameof(Double))
        {
            return ClientTemplate(key, nameof(EGridTemplate.Integer));
        }

        if (dataType == nameof(DateTime))
        {
            return ClientTemplate(key, nameof(EGridTemplate.DateTime));
        }

        return ClientTemplate(key, nameof(EGridTemplate.Text));
    }

    public static string GetGridTemplate(string memberKey, string template)
    {
        string CreateKendoTemplate(Queue<string> queue, string? previous = null)
        {
            var dequeued = queue.Dequeue();
            var dequeueKey = previous == null
                ? dequeued
                : $"{previous}.{dequeued}";

            if (!queue.Any())
            {
                return $"#{KeyTemplate(dequeueKey, template)}#";
            }

            return $"if({dequeueKey}!==undefined && {dequeueKey}!=null){{ {CreateKendoTemplate(queue, dequeueKey)} }}";
        }

        var collection = memberKey.Split(".")
                                  .ToList();

        var kendoTemplate = $"#{CreateKendoTemplate(new Queue<string>(collection))}#";

        return kendoTemplate;
    }

    public static string? KeyTemplate(string key, string kendoGridTemplate, string? value = null)
    {
        return kendoGridTemplate switch
        {
            nameof(EGridTemplate.Currency) => $"#=kendo.toString({key}, \"c{value ?? "2"}\")#",
            nameof(EGridTemplate.Exponential) => $"#=kendo.toString({key}, \"e{value ?? "2"}\")#",
            nameof(EGridTemplate.Hours) => $"#=kendo.toString({key}, \"e{value ?? "2"}\")#",
            nameof(EGridTemplate.GreaterThanZero) => $"#if({key}>0){{##=kendo.toString({key}, \"e{value ?? "2"}\")##}}#",
            nameof(EGridTemplate.Date) => $"#= {key} !== null ? kendo.toString(kendo.parseDate({key}),\"yyyy-MM-dd\") : ''#",
            nameof(EGridTemplate.DateTime) => $"#=kendo.toString(kendo.parseDate({key}),\"MMM dd, yyyy at h:mm tt\")#",
            nameof(EGridTemplate.MilitaryTime) => $"#=kendo.toString({key},\"H:mm\")#",
            nameof(EGridTemplate.MilitaryDateTime) => $"#=kendo.toString({key},\"yyyy-MM-dd H:mm\")#",
            nameof(EGridTemplate.SplitCamelCase) => $"#= {key}!==null ? {key}.replace(/([a-zA-Z])([A-Z])([a-z])/g, '$1 $2$3') : ''#",
            nameof(EGridTemplate.Text) => $"#= {key} !== null ? {key} : '' #",
            nameof(EGridTemplate.MinutesToTime) => $"#= Math.floor({key} / 60).toString().padStart(2, '0') #:#=({key} % 60).toString().padStart(2, '0')#",
            nameof(EGridTemplate.Phone) => $"#=kendo.toString( {key} !== null ? {key} : '',\"(\\#\\#\\#)-\\#\\#\\#-\\#\\#\\#\\#\")#",
            nameof(EGridTemplate.TimeMonthDay) => $"#=kendo.toString(kendo.parseDate({key}),\"h:mm MMM dd\")#",
            nameof(EGridTemplate.YesNo) => $"#if({key}===true||{key}==='true'){{#Yes#}}else{{#No#}}#",
            nameof(EGridTemplate.Yes) => $"#if({key}===true||{key}==='true'){{#Yes#}}#",
            nameof(EGridTemplate.NotYes) => $"#if({key}===false||{key}==='false'){{#Yes#}}#",
            nameof(EGridTemplate.Email) => $"#={key}#",
            nameof(EGridTemplate.Integer) => $"#=kendo.toString({key}, \"n0\")#",
            nameof(EGridTemplate.GreaterThan0) => $"#if({key}>0){{##={key}##}}#",
            nameof(EGridTemplate.Number) => $"#=kendo.toString({key}, \"n{value ?? "2"}\")#",
            nameof(EGridTemplate.Percent) => $"#=kendo.toString({key},\"0.00%\")#",
            nameof(EGridTemplate.Token) => $"\\##={key}#",
            nameof(EGridTemplate.Template) => throw new ArgumentOutOfRangeException(nameof(kendoGridTemplate), kendoGridTemplate, null),
            _ => null
        };
    }

    public static List<FormField> ListFormFields(Type t, object? obj)
    {
        obj ??= Activator.CreateInstance(t);

        return ListFormFields(obj!);
    }

    public static List<FormField> ListFormFields(object obj, string childSeparator = ".")
    {
        if (!obj.GetType()
                .IsClass)
        {
            throw new InvalidOperationException("Object is a struct but a class is required. (Error: b9687199-a422-46ca-bf67-c2db9a5eae37)");
        }

        var list = new List<FormField>();
        var properties = obj.GetType()
                            .GetProperties();
        foreach (var propertyInfo in properties)
        {
            var property = GetFormField(obj, propertyInfo, childSeparator);
            if (property != null)
            {
                list.Add(property);
            }
        }

        return list.ToList();
    }

    public static List<FormField> ListFormFields<TType>()
    {
        return ListFormFields(typeof(TType));
    }

    public static List<FormField> ListFormFields(Type t, string childSeparator)
    {
        var list = new List<FormField>();
        var properties = t.GetProperties();
        foreach (var propertyInfo in properties)
        {
            var hasAttribute = HasAttribute<HtmlAttributeNotBoundAttribute>(propertyInfo);
            if (!hasAttribute)
            {
                var property = GetFormField(propertyInfo, propertyInfo.DeclaringType?.Name, null, childSeparator);
                if (property != null)
                {
                    list.Add(property);
                }
            }
        }

        return list.ToList();
    }

    public static string? ParseString(object? o)
    {
        if (o == null)
        {
            return null;
        }

        if (o is string s)
        {
            return s;
        }

        return o.ToString();
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

    private static List<SelectListItem>? GetBindData(PropertyInfo propertyInfo)
    {
        var attributes = (DropdownOptionAttribute[])Attribute.GetCustomAttributes(propertyInfo, typeof(DropdownOptionAttribute));

        if (!attributes.Any())
        {
            return null;
        }

        return attributes.Select(a => new SelectListItem(a.Name, a.Value))
                         .ToList();
    }

    private static string? GetCascadeFrom(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<CascadeFromAttribute>()
                           ?.CascadeFrom;
    }

    private static int? GetColumnWidth(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<GridColumnWidthAttribute>();

        return attribute?.Width;
    }

    private static string GetDataType(PropertyInfo propertyInfo)
    {
        return propertyInfo.PropertyType.Name;
    }

    private static string? GetDefaultValue(PropertyInfo propertyInfo)
    {
        var value = propertyInfo.GetCustomAttribute<DefaultValueAttribute>()
                                ?.Value;

        return value != null
            ? value.ToString()
            : CalculatedDefaultValue(propertyInfo);
    }

    private static string? GetDescription(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<DescriptionAttribute>()
                           ?.Description;
    }

    private static string? GetErrorMessage(PropertyInfo propertyInfo)
    {
        string? message = null;

        var requiredAttribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();
        if (requiredAttribute != null)
        {
            if (string.IsNullOrEmpty(requiredAttribute.ErrorMessage))
            {
                message = StringUtil.SplitCamelCase(propertyInfo.Name) + " is required";
            }
            else
            {
                message = requiredAttribute.ErrorMessage;
            }
        }

        return message;
    }

    // private static void GetForeignKey(PropertyInfo propertyInfo, FormField property)
    // {
    //     var hasAttribute = HasAttribute<ForeignKeyAttribute>(propertyInfo);
    //     if (!hasAttribute)
    //     {
    //         return;
    //     }
    //
    //     var propertyName = propertyInfo.GetCustomAttribute<ForeignKeyAttribute>()!.Name;
    //     property.ForeignKey = propertyInfo.DeclaringType?.GetProperty(propertyName)
    //                                       ?.PropertyType!;
    //     property.Name = propertyName;
    // }

    private static string? GetFormTag(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<FormTagAttribute>()
                           ?.Tag;
    }

    private static bool GetHideLabel(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<FormFieldAttribute>();

        return attribute is
        {
            HideLabel: true
        };
    }

    private static string GetInputTemplate(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<HiddenInputAttribute>();
        if (attribute != null)
        {
            return "Hidden";
        }

        var eInputTemplate = propertyInfo.GetCustomAttribute<FormFieldAttribute>()
                                         ?.FormField;
        if (eInputTemplate != null)
        {
            return eInputTemplate;
        }

        var dataType = GetDataType(propertyInfo);

        return GetInputTemplateFromDataType(dataType);
    }

    private static string GetInputTemplateFromDataType(string? dataType)
    {
        return dataType switch
        {
            "DateTime" => nameof(EFormField.DateTime),
            "Boolean" => nameof(EFormField.YesNo),
            "String" => nameof(EFormField.Text),
            "Int32" => nameof(EFormField.Integer),
            "Int64" => nameof(EFormField.Integer),
            "Decimal" => nameof(EFormField.Decimal),
            "Double" => nameof(EFormField.Decimal),
            _ => "Object"
        };
    }

    private static string GetLabel(PropertyInfo propertyInfo)
    {
        var displayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
        if (displayName != null)
        {
            return displayName.DisplayName;
        }

        var display = propertyInfo.GetCustomAttribute<DisplayAttribute>();

        var splitCamelCase = display != null
            ? display.Name ?? StringUtil.SplitCamelCase(propertyInfo.Name)
            : StringUtil.SplitCamelCase(propertyInfo.Name);

        return splitCamelCase;
    }

    private static void GetLayout(FormField formFieldProperty, PropertyInfo propertyInfo)
    {
        var rowAttribute = propertyInfo.GetCustomAttribute<RowAttribute>();
        var colAttribute = propertyInfo.GetCustomAttribute<ColAttribute>();

        if (rowAttribute != null && colAttribute == null)
        {
            colAttribute = new ColAttribute(0);
        }

        if (rowAttribute == null && colAttribute != null)
        {
            rowAttribute = new RowAttribute(0, 0);
        }

        formFieldProperty.Col = colAttribute?.ColumnStart ?? 0;
        formFieldProperty.Row = rowAttribute?.RowStart ?? 0;
    }

    private static string? GetPlaceholder(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<PlaceholderAttribute>()
                           ?.Placeholder;
    }

    private static PropertyInfo? GetPropertyInfo(Type t, string name)
    {
        return t.GetProperties()
                .SingleOrDefault(w => w.Name == name);
    }

    private static bool GetReadOnly(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<ReadOnlyAttribute>();
        if (attribute == null)
        {
            return false;
        }

        return attribute.IsReadOnly;
    }

    private static bool GetRequired(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<RequiredAttribute>() != null;
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

    private static bool IsClass(PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType.FullName != null && propertyInfo.PropertyType.FullName.StartsWith("System."))
        {
            return false;
        }

        if (propertyInfo.PropertyType.FullName != null && propertyInfo.PropertyType.FullName.StartsWith("System.Nullable"))
        {
            return false;
        }

        return true;
    }

    private static List<FormField>? ListChildren(PropertyInfo propertyInfo, string propertyInfoName, object? obj, string childSeparator)
    {
        if (obj == null)
        {
            return null;
        }

        return ListChildSortedProperties(propertyInfo.PropertyType, propertyInfoName, obj, childSeparator);
    }

    private static List<FormField> ListChildSortedProperties(Type t, string propertyInfoName, object obj, string childSeparator)
    {
        var list = new List<FormField>();
        var properties = t.GetProperties();
        foreach (var propertyInfo in properties)
        {
            var value = GetValue(obj, propertyInfo);

            var hasNotBoundAttribute = HasAttribute<HtmlAttributeNotBoundAttribute>(propertyInfo);
            if (!hasNotBoundAttribute)
            {
                var property = GetFormField(propertyInfo, propertyInfoName, value, childSeparator);
                if (property != null)
                {
                    list.Add(property);
                }
            }
        }

        return list.ToList();
    }
}