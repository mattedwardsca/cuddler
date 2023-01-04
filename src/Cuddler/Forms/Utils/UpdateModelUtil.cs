using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Forms.Utils;

public static class UpdateModelUtil
{
    public static void UpdateModelValue<TModel>([DisallowNull] TModel model, string key, object? value)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var prop = model.GetType()
                        .GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            if (value == null)
            {
                prop.SetValue(model, null, null);
            }
            else
            {
                SetPropertyValueFromDictionaryValue(model, prop, value);
            }
        }
    }

    public static void UpdateModelValues<TModel>(TModel model, IDictionary<string, object?> formDictionary)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (formDictionary == null)
        {
            throw new ArgumentNullException(nameof(formDictionary));
        }

        foreach (var (key, value) in formDictionary)
        {
            if (key.Equals("DateCreated", StringComparison.InvariantCultureIgnoreCase) || key.Equals("DateUpdated", StringComparison.InvariantCultureIgnoreCase) || key.Equals("Id", StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }

            UpdateModelValue(model, key, value);
        }
    }

    public static void UpdateModelValues<TModel>(TModel model, IFormCollection formDictionary) where TModel : class
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (formDictionary == null)
        {
            throw new ArgumentNullException(nameof(formDictionary));
        }

        foreach (var (key, value) in formDictionary)
        {
            if (key.Equals("DateCreated", StringComparison.InvariantCultureIgnoreCase) || key.Equals("DateUpdated", StringComparison.InvariantCultureIgnoreCase) || key.Equals("Id", StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }

            UpdateModelValue(model, key, string.IsNullOrEmpty(value)
                ? null
                : value.ToString());
        }
    }

    private static void SetAssignableProperty<TModel>(TModel model, PropertyInfo prop, object? item)
    {
        var val = Convert.ChangeType(item, prop.PropertyType);
        prop.SetValue(model, val, null);
    }

    private static void SetBoolProperty<TModel>(TModel model, PropertyInfo prop, object? item)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        bool? b;
        {
            var s = item?.ToString();
            if (s == null)
            {
                b = null;
            }
            else
            {
                b = s.Equals("on")
                    || s.ToLower()
                        .Equals("true");
            }
        }

        prop.SetValue(model, b, null);
    }

    private static void SetDateProperty<TModel>(TModel model, PropertyInfo prop, object? stringValue)
    {
        try
        {
            var dateTime = stringValue == null
                ? (DateTime?)null
                : DateTime.Parse((string)stringValue, new CultureInfo("en-US"));
            prop.SetValue(model, dateTime, null);
        }
        catch (Exception)
        {
            prop.SetValue(model, null, null);

            throw;
        }
    }

    private static void SetDecimalProperty<TModel>(TModel model, PropertyInfo prop, object? decimalValue)
    {
        var number = decimalValue == null
            ? (decimal?)null
            : decimal.Parse((string)decimalValue, new CultureInfo("en-US"));
        prop.SetValue(model, number, null);
    }

    private static void SetDoubleProperty<TModel>(TModel model, PropertyInfo prop, object? doubleValue)
    {
        var number = doubleValue == null
            ? (double?)null
            : double.Parse((string)doubleValue, new CultureInfo("en-US"));
        prop.SetValue(model, number, null);
    }

    private static void SetIntegerProperty<TModel>(TModel model, PropertyInfo prop, object? intValue)
    {
        var number = intValue == null
            ? (int?)null
            : int.Parse((string)intValue, new CultureInfo("en-US"));
        prop.SetValue(model, number, null);
    }

    private static void SetEnum<TModel>(TModel model, PropertyInfo prop, object? stringValue)
    {
        prop.SetValue(model, stringValue, null);
    }

    private static void SetNullableProperty<TModel>(TModel model, PropertyInfo prop, object? stringValue)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var genericArgument = prop.PropertyType.GetGenericArguments()[0];
        if (genericArgument == typeof(DateTime))
        {
            SetDateProperty(model, prop, stringValue);
        }
        else if (genericArgument == typeof(double))
        {
            SetDoubleProperty(model, prop, stringValue);
        }
        else if (genericArgument == typeof(bool))
        {
            if (stringValue == null || string.IsNullOrEmpty((string)stringValue) || "NULL".Equals(stringValue))
            {
                SetBoolProperty(model, prop, null);
            }
            else
            {
                SetBoolProperty(model, prop, stringValue);
            }
        }
        else if (genericArgument == typeof(decimal))
        {
            SetDecimalProperty(model, prop, stringValue);
        }
        else if (genericArgument == typeof(int))
        {
            SetIntegerProperty(model, prop, stringValue);
        }
        else if (genericArgument.IsEnum)
        {
            SetEnum(model, prop, stringValue);
        }
        else
        {
            throw new ArgumentException($"Type [{genericArgument}] for property [{model.GetType()}.{prop.Name}] is unsupported. Error: 08c8eab0-d50f-495e-b2c5-cd8ab75ac3d7");
        }
    }

    private static void SetPropertyValueFromDictionaryValue<TModel>(TModel model, PropertyInfo prop, object? item)
    {
        var stringValue = item?.ToString();
        if (string.IsNullOrEmpty(stringValue))
        {
            prop.SetValue(model, null, null);
        }
        else
        {
            var isNullableProperty = prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (isNullableProperty)
            {
                SetNullableProperty(model, prop, item);
            }
            else if (prop.PropertyType == typeof(string[]))
            {
                SetStringArrayProperty(model, prop, item);
            }
            else if (prop.PropertyType == typeof(bool))
            {
                SetBoolProperty(model, prop, item);
            }
            else if (prop.PropertyType == typeof(decimal))
            {
                SetAssignableProperty(model, prop, item);
            }
            else if (prop.PropertyType == typeof(int))
            {
                SetAssignableProperty(model, prop, item);
            }
            else if (prop.PropertyType == typeof(float))
            {
                SetAssignableProperty(model, prop, item);
            }
            else if (prop.PropertyType == typeof(DateTime))
            {
                var dateString = item?.ToString();
                if (dateString != null && DateTime.TryParse(dateString, out var parsedDate))
                {
                    SetAssignableProperty(model, prop, parsedDate);
                }

            }
            else
            {
                SetSimpleProperty(model, prop, item);
            }
        }
    }

    private static void SetSimpleProperty<TModel>(TModel model, PropertyInfo prop, object? item)
    {
        if (item == null)
        {
            return;
        }

        prop.SetValue(model, item, null);

    }

    private static void SetStringArrayProperty<TModel>(TModel model, PropertyInfo prop, object? item)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (item == null)
        {
            prop.SetValue(model, null, null);
        }
        else
        {
            var s = item.ToString();
            if (s == null)
            {
                prop.SetValue(model, null, null);
            }
            else
            {
                var vl = s.Split(',');
                prop.SetValue(model, vl, null);
            }
        }
    }
}