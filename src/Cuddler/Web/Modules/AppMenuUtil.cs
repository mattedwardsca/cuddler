using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Modules;

public static class AppMenuUtil
{
    public static IMenuItem? GetAppRootLink(List<IMenuItem> menu)
    {
        menu = menu.Where(w => w.LinkType == ELinkType.Link)
                   .ToList();
        var links = new List<IMenuItem>();
        foreach (var menuLink in menu)
        {
            links.Add(menuLink);
        }

        foreach (var menuLink in links)
        {
            if (!menuLink.Hide)
            {
                return menuLink;
            }
        }

        return null;
    }

    public static string? ConvertToPath(string? title)
    {
        if (title == null)
        {
            return null;
        }

        title = title.Trim();
        title = title.Replace(" ", string.Empty);
        title = title.Replace(":", string.Empty);
        title = title.Replace(";", string.Empty);
        title = title.Replace(",", string.Empty);
        title = title.Replace("'", string.Empty);
        title = title.Replace("\"", string.Empty);
        title = title.Replace("\"", string.Empty);
        title = title.Replace("/", string.Empty);
        title = title.Replace("-of-", "Of");
        title = title.Replace("-", string.Empty);
        title = title.Replace("&", "And");

        return title;
    }

    public static string GetPageUrl(HttpContext context)
    {
        return context.Request.PathBase.Value + context.Request.Path.Value;
    }

    public static bool IsHomepage(HttpContext context, string? menuPath)
    {
        if (string.IsNullOrEmpty(menuPath))
        {
            return false;
        }

        var pathValue = $"{context.Request.PathBase}{context.Request.Path}";
        if (!pathValue.EndsWith("/"))
        {
            pathValue += "/";
        }

        if (!menuPath.EndsWith("/"))
        {
            menuPath += "/";
        }

        return pathValue.Equals(menuPath, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool IsSelected(string pathValue, string? menuPath)
    {
        if (string.IsNullOrEmpty(menuPath))
        {
            return false;
        }

        if (!pathValue.EndsWith("/"))
        {
            pathValue += "/";
        }

        if (!menuPath.EndsWith("/"))
        {
            menuPath += "/";
        }

        return pathValue.StartsWith(menuPath, StringComparison.InvariantCultureIgnoreCase);
    }
}