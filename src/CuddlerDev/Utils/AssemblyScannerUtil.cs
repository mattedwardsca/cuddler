using System.Reflection;

namespace CuddlerDev.Utils;

public static class AssemblyScannerUtil
{
    public static TReturn CreateInstance<TReturn>(Type type)
    {
        return CreateInstance<TReturn>(type, null);
    }

    public static TReturn CreateInstance<TReturn>(Type type, params object[]? obj)
    {
        return (TReturn)Activator.CreateInstance(type, obj)!;
    }

    public static Type? Find<T>(string name)
    {
        var listAllOfType = ListAllOfType<T>();

        return listAllOfType.SingleOrDefault(w => w.Name == name);
    }

    public static List<Type> GetAllOfType<T>()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                        .Select(x => x)
                        .ToList();
    }

    public static List<Type> GetAllOfType<T>(Assembly assembly)
    {
        return assembly.GetTypes()
                       .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                       .Select(x => x)
                       .ToList();
    }

    public static TType? GetAttribute<TType>(Type t) where TType : Attribute
    {
        if (t.IsDefined(typeof(TType), false))
        {
            return (TType)Attribute.GetCustomAttribute(t, typeof(TType))!;
        }

        return null;
    }


    public static TType? GetAttribute<TType>(Type t, string propertyName) where TType : Attribute
    {
        var list = propertyName.Split(".")
                               .ToList();
        var queue = new Queue<string>(list);
        PropertyInfo? propertyInfo = null;
        while (queue.Any())
        {
            var item = queue.Dequeue();

            if (propertyInfo == null)
            {
                propertyInfo = t.GetProperty(item);
            }
            else
            {
                var type = propertyInfo.PropertyType;
                propertyInfo = type.GetProperty(item);
            }
        }

        return propertyInfo?.GetCustomAttribute<TType>();
    }

    public static IEnumerable<Type> GetTypesWithAttribute<T>()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly1 in assemblies)
        foreach (var type in assembly1.GetTypes())
        {
            if (type.GetCustomAttributes(typeof(T), true)
                    .Length
                > 0)
            {
                yield return type;
            }
        }
    }

    public static IEnumerable<TReturn> InstantiateAllOfType<T, TReturn>(params object[]? obj)
    {
        var instantiateAllOfType = ListAllOfType<T>();

        foreach (var type in instantiateAllOfType)
        {
            yield return CreateInstance<TReturn>(type, obj)!;
        }
    }

    public static IEnumerable<TReturn> InstantiateAllOfType<T, TReturn>()
    {
        return InstantiateAllOfType<T, TReturn>(null);
    }

    public static IEnumerable<T> InstantiateAllOfType<T>()
    {
        return InstantiateAllOfType<T, T>(null);
    }

    public static IEnumerable<Type> ListAllOfType<T>()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                        .Select(x => x)
                        .ToList();
    }

    public static IEnumerable<Type> ListTypesWithAttribute<TType, TAttribute>()
    {
        return ListTypesWithAttribute<TAttribute>(typeof(TType));
    }

    public static IEnumerable<Type> ListTypesWithAttribute<TAttribute>(Type t)
    {
        var assembly = t.Assembly;

        foreach (var type in assembly.GetTypes())
        {
            if (type.GetCustomAttributes(typeof(TAttribute), true)
                    .Length
                > 0)
            {
                yield return type;
            }
        }
    }
}