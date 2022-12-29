using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Cuddler.Utils;

public static class ErrorModelUtil
{
    public static IDictionary<string, string> GetModelValidationErrors<TModel>(TModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var declaredProperties = model.GetType()
                                      .GetTypeInfo()
                                      .DeclaredProperties;

        return (from propertyInfo in declaredProperties
                let isRequired = GetIsRequired(propertyInfo)
                where isRequired
                let value = ReflectionsGetProperty(model, propertyInfo.Name)
                where value == null
                select propertyInfo.Name).ToDictionary(name => name, name => $"{name} is required");
    }

    public static IDictionary<string, string> GetValidationErrors<TEntity>(TEntity item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var declaredProperties = item.GetType()
                                     .GetTypeInfo()
                                     .DeclaredProperties;

        return (from propertyInfo in declaredProperties
                let isRequired = GetIsRequired(propertyInfo)
                where isRequired
                let value = ReflectionsGetProperty(item, propertyInfo.Name)
                where value == null
                select propertyInfo.Name).ToDictionary(name => name, name => $"{name} is required");
    }

    private static bool GetIsRequired(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

        return attribute != null;
    }

    private static object? ReflectionsGetProperty(object obj, string name)
    {
        var methodInfo = obj.GetType()
                            .GetProperty(name)
                            ?.GetMethod;
        if (methodInfo != null)
        {
            var results = methodInfo.Invoke(obj, Array.Empty<object>());

            return results;
        }

        return null;
    }

    public static IDictionary<string, string> Add(string key, string value)
    {
        var dictionary = new Dictionary<string, string>
        {
            { key, value }
        };

        return dictionary;
    }
}