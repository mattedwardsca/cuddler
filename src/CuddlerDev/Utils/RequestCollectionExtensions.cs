using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Utils;

public static class RequestCollectionExtensions
{
    public static bool GetBool(this IQueryCollection collection, string key)
    {
        var isFound = collection.TryGetValue(key, out var paramValue);
        var isChecked = isFound && bool.Parse(paramValue.First()!);

        return isChecked;
    }

    public static DateTime? GetDate(this IQueryCollection collection, string key)
    {
        collection.TryGetValue(key, out var values);
        var firstOrDefault = values.FirstOrDefault();

        if (string.IsNullOrEmpty(firstOrDefault))
        {
            return null;
        }

        return ParseDatetime(firstOrDefault);
    }

    public static string? GetValue(this IQueryCollection collection, string key)
    {
        collection.TryGetValue(key, out var values);

        return values.FirstOrDefault();
    }

    public static string? GetValue(this IFormCollection collection, string key)
    {
        collection.TryGetValue(key, out var values);

        return values.FirstOrDefault();
    }

    public static string[] GetValues(this IQueryCollection collection, string key)
    {
        collection.TryGetValue(key, out var values);

        return values.ToArray()!;
    }

    private static DateTime? ParseDatetime(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new DateTime?();
        }

        return DateTime.Parse(value, CultureInfo.InvariantCulture); //Invariant culture because if this runs on a Canadian server vs American server, this will completely change how it is parsed.
    }
}