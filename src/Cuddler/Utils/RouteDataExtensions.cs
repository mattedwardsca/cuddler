using Microsoft.AspNetCore.Routing;

namespace Cuddler.Utils;

public static class RouteDataExtensions
{
    public static string? GetOptional(this RouteData data, string name)
    {
        return data.Values[name] == null
            ? null
            : data.Values[name]!.ToString();
    }

    public static string GetRequired(this RouteData data, string name)
    {
        return data.Values[name] == null
            ? throw new ArgumentException($"{name} is missing from the route.")
            : data.Values[name]!.ToString()!;
    }

    public static string GetPageId(this RouteData data)
    {
        return GetRequired(data, "PageId");
    }
}