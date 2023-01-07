using System.Globalization;
using System.Reflection;
using Cuddler.Data.Utils;
using Cuddler.Utils;
using Cuddler.Web.Settings;

namespace Cuddler.Configuration;

internal class SettingsService : ISettingsService
{
    private readonly ApplicationSettings _applicationSettings;
    private readonly WebsiteSettings _websiteSettings;

    public SettingsService(WebsiteSettings websiteSettings, ApplicationSettings applicationSettings)
    {
        _websiteSettings = websiteSettings;
        _applicationSettings = applicationSettings;
    }

    public void InitWebsiteSettings(WebsiteSettings instance)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        var globaSettingsPath = $@"{_applicationSettings.ContentRootFolder}\WebsiteSettings.json";
        if (File.Exists(globaSettingsPath))
        {
            var json = File.ReadAllText(globaSettingsPath);
            var websiteSettings = SerializationUtil.JsonDeserializeObject(_websiteSettings.GetType(), json);
            ObjectCopierUtil.CopyProperties(websiteSettings, _websiteSettings);
        }
    }

    public async Task SaveValue(string key, string? value)
    {
        object? instance = _websiteSettings;
        object? parent = null;
        PropertyInfo? property = null;
        var path = key.Split('.');
        for (var index = 1; index < path.Length; index++)
        {
            var s = path[index];
            property = ReflectionUtil.GetProperty(instance!, s)!;
            parent = instance;
            instance = property.GetValue(instance);
        }

        if (property == null)
        {
            return;
        }

        SetNullableProperty(parent, property, value);

        var json = SerializationUtil.JsonSerializeObject(_websiteSettings);
        var globaSettingsPath = $@"{_applicationSettings.ContentRootFolder}\WebsiteSettings.json";
        await File.WriteAllTextAsync(globaSettingsPath, json);
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
                b = s.Equals("on") || s.ToLower().Equals("true");
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

    private static void SetEnum<TModel>(TModel model, PropertyInfo prop, object? stringValue)
    {
        prop.SetValue(model, stringValue, null);
    }

    private static void SetIntegerProperty<TModel>(TModel model, PropertyInfo prop, object? intValue)
    {
        var number = intValue == null
            ? (int?)null
            : int.Parse((string)intValue, new CultureInfo("en-US"));
        prop.SetValue(model, number, null);
    }

    private static void SetNullableProperty<TModel>(TModel model, PropertyInfo prop, object? stringValue)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var genericArgument = prop.GetMethod!.ReturnType;
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
}