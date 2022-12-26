using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Extensions;

public static class PathExtensions
{
    public static string CombinedPath(this HttpRequest request)
    {
        var combinedPath = $"{request.PathBase}{request.Path}";
        if (combinedPath.EndsWith("/"))
        {
            combinedPath = combinedPath[..^1];
        }

        return combinedPath;
    }

    public static bool EndsWith(this PathString path, string urlPart)
    {
        if (string.IsNullOrEmpty(urlPart))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(urlPart));
        }

        if (path.Value == null)
        {
            return false;
        }

        if (path.Value.ToLower()
                .EndsWith(urlPart.ToLower()))
        {
            return true;
        }

        if (!urlPart.EndsWith("/"))
        {
            var slashed = urlPart + "/";
            if (path.Value.EndsWith(slashed))
            {
                return true;
            }
        }

        return false;
    }

    public static string FullPath(this HttpRequest request)
    {
        var fullPath = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), request.PathBase.ToUriComponent(), request.Path.ToUriComponent(), request.QueryString.ToUriComponent());

        if (fullPath.EndsWith("/"))
        {
            fullPath = fullPath[..^1];
        }

        return fullPath.ToLower();
    }

    public static string GetFirstSegment(this PathString requestPath)
    {
        var segment = requestPath.Value!.Split('/')
                                 .FirstOrDefault(w => !string.IsNullOrEmpty(w));

        return segment ?? string.Empty;
    }

    public static string GetNakedDomain(HttpContext httpContext)
    {
        var subDomain = string.Empty;
        var host = httpContext.Request.Host.Host;
        if (!string.IsNullOrWhiteSpace(host))
        {
            var ss = host.Split('.');
            if (ss.Length == 1)
            {
                subDomain = host;
            }
            else
            {
                subDomain = ss[^2] + "." + ss[^1];
            }
        }

        return subDomain.Trim()
                        .ToLower();
    }

    public static bool PathContains(this HttpRequest httpRequest, string partialUrl)
    {
        if (string.IsNullOrEmpty(partialUrl))
        {
            return false;
        }

        return httpRequest.Path.Value != null && httpRequest.Path.Value.Contains(partialUrl);
    }

    public static bool PathEndsWith(this HttpRequest httpRequest, string partialUrl)
    {
        if (string.IsNullOrEmpty(partialUrl))
        {
            return false;
        }

        return httpRequest.Path.Value != null && (httpRequest.Path.Value.EndsWith(partialUrl) || httpRequest.Path.Value.EndsWith($"{partialUrl}/"));
    }

    public static bool StartsWith(this HttpRequest request, params string[] pathString)
    {
        foreach (var path in pathString)
        {
            if (request.Path.StartsWithSegments(path, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public static bool StartsWith(this PathString path, string urlPart)
    {
        if (string.IsNullOrEmpty(urlPart))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(urlPart));
        }

        if (!path.HasValue)
        {
            return false;
        }

        var pathValue = path.Value ?? "";
        if (pathValue.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
        {
            pathValue = pathValue[1..];
        }

        if (urlPart.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
        {
            urlPart = urlPart[1..];
        }

        return pathValue.StartsWith(urlPart, StringComparison.InvariantCultureIgnoreCase);
    }
}