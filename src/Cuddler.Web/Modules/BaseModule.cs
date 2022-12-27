using Cuddler.Core.Services.Modules.Models;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Modules;

public abstract class BaseModule : IBoostModule
{
    private List<IClientApp>? _allApps;

    protected BaseModule(string siteName, string rootLink = "/")
    {
        SiteName = siteName;
        RootLink = rootLink;
    }

    public virtual List<IClientApp> HiddenApps { get; } = new();

    public string? Icon { get; set; }

    public virtual List<IClientApp> MiddleApps { get; } = new();

    public virtual List<IClientApp> TopApps { get; } = new();

    public async Task<string> GetRootPage(HttpRequest request)
    {
        var isAdminMode = request.HttpContext.IsAdminMode();
        var firstApp = GetFirstApp(isAdminMode);

        if (firstApp == null)
        {
            throw new InvalidOperationException("No apps have been registered.");
        }

        var menuItems = await firstApp.GetAppMenu(request.HttpContext);
        var url = string.Empty;
        var s = firstApp.Name.Replace(" ", string.Empty);
        if (!string.IsNullOrEmpty(s))
        {
            url += $"/{s}";
        }

        var pageSegment = menuItems.FirstOrDefault(w => w.LinkType == ELinkType.Link && !w.Hide)
                                   ?.Segment;
        if (!string.IsNullOrEmpty(pageSegment))
        {
            url += $"/{pageSegment}";
        }

        return url;
    }

    public string? Description { get; set; }

    public string? Slogan { get; set; }

    public string RootLink { get; }

    public bool HasApp(string appId)
    {
        if (_allApps == null)
        {
            return false;
        }

        return _allApps.Any(w => string.Equals(w.Name, appId, StringComparison.InvariantCultureIgnoreCase));
    }

    public List<IClientApp> ListAllApps()
    {
        if (_allApps != null)
        {
            return _allApps;
        }

        _allApps = new List<IClientApp>();
        _allApps.AddRange(TopApps);
        _allApps.AddRange(MiddleApps);
        _allApps.AddRange(HiddenApps);

        return _allApps;
    }

    public abstract string GetModuleSubscriptionId();

    public bool AllowAdminMode { get; set; }

    // ReSharper disable once UnassignedGetOnlyAutoProperty
    public bool RequiresSubscription { get; }

    public string SiteName { get; set; }

    private IClientApp? GetFirstApp(bool isAdminMode)
    {
        var firstOrDefault = isAdminMode
            ? TopApps.FirstOrDefault(w => w.IsForAdmins)
            : TopApps.FirstOrDefault(w => w.IsForClients);

        if (firstOrDefault == null)
        {
            firstOrDefault = isAdminMode
                ? MiddleApps.FirstOrDefault(w => w.IsForAdmins)
                : MiddleApps.FirstOrDefault(w => w.IsForClients);
        }

        if (firstOrDefault == null)
        {
            firstOrDefault = isAdminMode
                ? HiddenApps.FirstOrDefault(w => w.IsForAdmins)
                : HiddenApps.FirstOrDefault(w => w.IsForClients);
        }

        return firstOrDefault;

    }
}