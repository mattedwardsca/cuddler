using System.Reflection;

namespace CuddlerDev.Utils;

public static class ObjectCopierUtil
{
    public static void CopyDictionary(Dictionary<string, object?> source, object destination)
    {
        if (source == null || destination == null)
        {
            throw new Exception("Source or/and Destination Objects are null");
        }

        var typeDest = destination.GetType();
        foreach (var (key, value) in source)
        {
            var targetProperty = typeDest.GetProperty(key)!;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (targetProperty == null)
            {
                continue;
            }

            if (!targetProperty.CanWrite)
            {
                continue;
            }

            if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true)!.IsPrivate)
            {
                continue;
            }

            if ((targetProperty.GetSetMethod()!.Attributes & MethodAttributes.Static) != 0)
            {
                continue;
            }

            // Passed all tests, lets set the value
            targetProperty.SetValue(destination, value, null);
        }
    }

    /// <summary>
    ///     Extension for 'Object' that copies the properties to a destination object.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public static void CopyProperties(object source, object destination)
    {
        // If any this null throw an exception
        if (source == null || destination == null)
        {
            throw new Exception("Source or/and Destination Objects are null");
        }

        // Getting the Types of the objects
        var typeDest = destination.GetType();
        var typeSrc = source.GetType();

        // Iterate the Properties of the source instance and
        // populate them from their desination counterparts
        var srcProps = typeSrc.GetProperties();
        foreach (var srcProp in srcProps)
        {
            if (!srcProp.CanRead)
            {
                continue;
            }

            var targetProperty = typeDest.GetProperty(srcProp.Name)!;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (targetProperty == null)
            {
                continue;
            }

            if (!targetProperty.CanWrite)
            {
                continue;
            }

            if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true)!.IsPrivate)
            {
                continue;
            }

            if ((targetProperty.GetSetMethod()!.Attributes & MethodAttributes.Static) != 0)
            {
                continue;
            }

            if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
            {
                continue;
            }

            // Passed all tests, lets set the value
            targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
        }
    }
}