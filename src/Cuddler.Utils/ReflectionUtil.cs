using System.Linq.Expressions;
using System.Reflection;

namespace Cuddler.Utils;

public static class ReflectionUtil
{
    public static TType? GetAttribute<TType>(Type t) where TType : Attribute
    {
        if (t.IsDefined(typeof(TType), false))
        {
            return (TType)Attribute.GetCustomAttribute(t, typeof(TType))!;
        }

        return null;
    }

    public static MemberExpression GetMemberInfo<TObject>(Expression<Func<TObject, object>> method)
    {
        if (method is not LambdaExpression lambda)
        {
            throw new ArgumentNullException(nameof(method));
        }

        MemberExpression? memberExpr = null;

        if (lambda.Body.NodeType == ExpressionType.Convert)
        {
            memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
        }
        else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            memberExpr = lambda.Body as MemberExpression;
        }

        if (memberExpr == null)
        {
            throw new ArgumentException(nameof(method));
        }

        return memberExpr;
    }

    public static object? GetMethod(object obj, string name)
    {
        var results = obj.GetType()
                         .GetProperty(name)
                         ?.GetMethod?.Invoke(obj, Array.Empty<object>());

        return results;
    }

    public static PropertyInfo? GetProperty(object obj, string name)
    {
        var results = obj.GetType()
                         .GetProperty(name);

        return results;
    }

    public static string? GetPropertyAsString(object obj, string propertyName)
    {
        var result = obj.GetType()
                        .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        ?.GetMethod?.Invoke(obj, Array.Empty<object>());

        return result?.ToString();
    }

    public static object? GetPropertyValue(object obj, string propertyName)
    {
        var result = obj.GetType()
                        .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        ?.GetMethod?.Invoke(obj, Array.Empty<object>());

        return result;
    }

    public static bool HasAttribute<T>(PropertyInfo propertyInfo)
    {
        return Attribute.IsDefined(propertyInfo, typeof(T));
    }

    public static bool Implements<T>(object obj)
    {
        return obj.GetType().IsAssignableFrom(typeof(T));
    }
}