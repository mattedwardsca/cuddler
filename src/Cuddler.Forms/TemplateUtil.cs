using System.Linq.Expressions;
using System.Reflection;
using Cuddler.Forms.Attributes;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cuddler.Forms;

public static class TemplateUtil
{
    public const string DeleteButton = "<button type=\"button\" class=\"btn btn-icon btn-icon-danger p-inherit k-grid-delete\" tabindex=\"-1\"><span><i class=\"fas fa-trash fa-fw\"></i></span></button>";
    public const string ViewIcon = "<i class=\"fas fa-edit font-size-16\"></i>";

    public static object TextBold => new
    {
        @class = "text-bold"
    };

    public static object TextCenter => new
    {
        @class = "text-center"
    };

    public static object TextRight => new
    {
        @class = "text-right"
    };

    public static object Width100 => new
    {
        style = "width:100%;"
    };

    public static string? ClientTemplate<TType>(Expression<Func<TType, object?>> property, EGridTemplate kendoGridTemplate)
    {
        var key = GetKey(property);
        return ClientTemplate(key, kendoGridTemplate.ToString());

    }

    public static string? ClientTemplate(string key, string kendoGridTemplate)
    {
        return KeyTemplate(key, kendoGridTemplate);
    }

    public static string? ClientTemplate(string key, EGridTemplate kendoGridTemplate, string? value)
    {
        return KeyTemplate(key, kendoGridTemplate.ToString(), value);
    }

    public static IHtmlContent EditLink(string href)
    {
        return new HtmlString($"<a href=\"{href}\">{ViewIcon}</a>");
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

    public static string GetKey<TType>(Expression<Func<TType, object?>> property)
    {
        var propertyBody = property.Body.Print();   
        propertyBody = propertyBody.Replace("(object)", string.Empty);
        var firstPart = propertyBody.Split('.')
                                    .First();

        var key = propertyBody[(firstPart.Length + 1)..];

        return key;
    }

    public static string GetMember<TType>(Expression<Func<TType, object?>> property)
    {
        return GetKey(property);
    }

    public static string IfNotNull<TType>(Expression<Func<TType, object?>> property, string trueHtml, string? falseHtml = null)
    {
        var key = GetKey(property);

        if (falseHtml == null)
        {
            return "# if(" + IfNotNull(key) + "){#" + trueHtml + "#}#";
        }

        return "# if(" + IfNotNull(key) + "){#" + trueHtml + "#}else{#" + falseHtml + "#}#";
    }

    public static string IfTrue<TType>(Expression<Func<TType, object?>> property, string trueValue, string? falseValue = null)
    {
        var key = GetKey(property);

        if (falseValue == null)
        {
            return "# if(" + key + "){# " + trueValue + " #}#";
        }

        return "# if(" + key + "){# " + trueValue + " #}else{# " + falseValue + " #}#";
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

    public static int[] PageSizes()
    {
        int[] pageSizes =
        {
            10,
            50,
            100,
            1000
        };

        return pageSizes;
    }

    private static string GetDataType(PropertyInfo propertyInfo)
    {
        return propertyInfo.PropertyType.Name;
    }

    private static string IfNotNull(string key)
    {
        return $"{key}!==undefined && {key}!==null";
    }
}