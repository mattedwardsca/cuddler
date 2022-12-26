using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Cuddler.Core.Utils;

public static class ValidateModelUtil
{
    /// <summary>
    ///     Gets the model validation errors.
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    /// <param name="model">The model.</param>
    /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
    /// <exception cref="System.ArgumentNullException">model</exception>
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
                select propertyInfo).ToDictionary(s => s.Name, s => $"{s.Name} is required");
    }

    /// <summary>
    ///     Gets the is required.
    /// </summary>
    /// <param name="propertyInfo">The property information.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private static bool GetIsRequired(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

        return attribute != null;
    }

    /// <summary>
    ///     Reflectionses the get property.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="name">The name.</param>
    /// <returns>System.Nullable&lt;System.Object&gt;.</returns>
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
}