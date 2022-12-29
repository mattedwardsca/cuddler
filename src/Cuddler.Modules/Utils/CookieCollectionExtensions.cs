using Microsoft.AspNetCore.Http;

namespace Cuddler.Modules.Utils;

public static class CookieCollectionExtensions
{
    public static bool GetBool(this IRequestCookieCollection collection, string key)
    {
        return collection.TryGetValue(key, out var source) && !string.IsNullOrEmpty(source) && bool.Parse(source);
    }

    public static string? GetString(this IRequestCookieCollection collection, string key)
    {
        var tryGetValue = collection.TryGetValue(key, out var source);
        return !tryGetValue
            ? null
            : source;
    }
}