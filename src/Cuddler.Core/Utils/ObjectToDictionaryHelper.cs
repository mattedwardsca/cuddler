using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Utils;

public static class ObjectToDictionaryHelper
{
    public static Dictionary<string, object?> ConvertToDictonary(this IFormCollection form)
    {
        if (form == null)
        {
            throw new ArgumentException("Unable to convert object to a dictionary. The source object is null.");
        }

        var dictionary = new Dictionary<string, object?>();
        foreach (var (key, value) in form)
        {
            dictionary.Add(key, value.ToString());
        }

        return dictionary;
    }

    public static IDictionary<string, T?> ToDictionary<T>(this object? source)
    {
        if (source == null)
        {
            throw new ArgumentException("Unable to convert object to a dictionary. The source object is null.");
        }

        var dictionary = new Dictionary<string, T?>();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
        {
            AddPropertyToDictionary(property, source, dictionary);
        }

        return dictionary;
    }

    private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T?> dictionary)
    {
        var value = property.GetValue(source);
        if (IsOfType<T>(value!))
        {
            dictionary.Add(property.Name, (T)value!);
        }
    }

    private static bool IsOfType<T>(object value)
    {
        return value is T;
    }
}