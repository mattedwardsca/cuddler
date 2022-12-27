using System.Web;
using Cuddler.Web.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Core.Utils;

public static class ModulePageUtil
{
    public static async Task<string?> FormatPageTitle(IHtmlHelper htmlHelper, IBoostModule? module, IClientApp? app)
    {
        var str1 = htmlHelper.ViewData["Title"] as string;
        var links = app == null
            ? null
            : await app.GetAppMenu(htmlHelper.ViewContext.HttpContext);
        var menuItem = module == null || app == null
            ? null
            : GetSelectedPage(htmlHelper.ViewContext.HttpContext, app, links);
        var str2 = str1 ?? menuItem?.Text;

        return str2;
    }

    public static string GetPagePath(IClientApp app, IMenuItem? page)
    {
        if (page == null)
        {
            return $"/{app.Name.Replace(" ", string.Empty)}";
        }

        if (page.LinkType != ELinkType.Link)
        {
            throw new InvalidOperationException("There was an attempt to link to a divider. (Error: 2ed625ec-cf2b-46b7-9137-aa8a7460d2dd)");
        }

        return $"/{app.Name.Replace(" ", string.Empty)}/{page.Segment}";
    }

    public static IMenuItem? GetSelectedPage(HttpRequest request, IClientApp embeddedApp, bool preview, List<IMenuItem> menuLinks)
    {
        IMenuItem? currentMenuItem = null;
        if (preview)
        {
            request.Query.TryGetValue("page", out var values);
            var pagePath = values.FirstOrDefault();

            if (pagePath != null)
            {
                foreach (var menuItem in menuLinks.Where(w => w.LinkType == ELinkType.Link))
                {
                    var linkPath = $"/{embeddedApp.Name.Replace(" ", string.Empty)}/{menuItem.Segment}";
                    if (string.Equals(linkPath, pagePath, StringComparison.InvariantCultureIgnoreCase))
                    {
                        currentMenuItem = menuItem;
                    }
                }
            }
        }
        else
        {
            currentMenuItem = GetSelectedPage(request.HttpContext, embeddedApp, menuLinks);
        }

        return currentMenuItem;
    }

    public static IMenuItem? GetSelectedPage(HttpContext context, IClientApp app, List<IMenuItem>? pageLinks)
    {
        if (pageLinks != null)
        {
            foreach (var page in pageLinks.Where(w => w.LinkType == ELinkType.Link))
            {
                var pagePath = GetPagePath(app, page);
                if (IsSelected(context, pagePath))
                {
                    return page;
                }
            }
        }

        return null;
    }

    public static bool IsSelected(HttpContext context, string? menuPath)
    {
        if (string.IsNullOrEmpty(menuPath))
        {
            return false;
        }

        var pathValue = $"{context.Request.PathBase}{HttpUtility.UrlDecode(context.Request.Path)}";
        if (!pathValue.EndsWith("/"))
        {
            pathValue += "/";
        }

        if (!menuPath.EndsWith("/"))
        {
            menuPath += "/";
        }

        var selected = pathValue.StartsWith(menuPath, StringComparison.InvariantCultureIgnoreCase) || pathValue.StartsWith($"/{menuPath}", StringComparison.InvariantCultureIgnoreCase);

        return selected;
    }
}