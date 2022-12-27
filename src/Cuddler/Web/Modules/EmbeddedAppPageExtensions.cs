using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Modules;

public static class EmbeddedAppPageExtensions
{
    public static async Task<string> GetAppRootPage(this HttpRequest request)
    {
        var moduleFactory = request.HttpContext.GetService<IModuleService>();
        var app = moduleFactory.GetSegmentApp();
        if (app == null)
        {
            throw new InvalidOperationException($"Cannot find app for path: {request.Path}. Error: a7ed223b-5b03-49f5-b25f-b23f2ad8a3a7");
        }

        return await GetRootPage(app, request);
    }

    private static async Task<string> GetRootPage(IClientApp app, HttpRequest request)
    {
        var pages = (await app.GetAppMenu(request.HttpContext)).Where(w => w.LinkType == ELinkType.Link)
                                                               .ToList();

        foreach (var menuItem in pages)
        {
            return $"/{app.Name.Replace(" ", string.Empty)}/{menuItem.Segment}";
        }

        return $"/{app.Name.Replace(" ", string.Empty)}/{pages.FirstOrDefault()?.Segment}";
    }
}