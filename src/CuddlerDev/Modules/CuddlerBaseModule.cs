using CuddlerDev.Modules.Utils;
using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Modules;

public abstract class CuddlerBaseModule : ICuddlerModule
{
    private List<IApp>? _allApps;

    protected CuddlerBaseModule(string siteName, string rootLink = "/")
    {
        SiteName = siteName;
        RootLink = rootLink;
    }

    public virtual List<IApp> HiddenApps { get; } = new();

    public virtual List<IApp> MiddleApps { get; } = new();

    public string RootLink { get; }

    public bool ShowAdminToggle { get; set; }

    public string SiteName { get; set; }

    public virtual List<IApp> TopApps { get; } = new();

    public virtual List<IApp> BottomApps { get; } = new();

    public List<IApp> ListAllApps()
    {
        if (_allApps != null)
        {
            return _allApps;
        }

        _allApps = new List<IApp>();
        _allApps.AddRange(TopApps);
        _allApps.AddRange(BottomApps);
        _allApps.AddRange(MiddleApps);
        _allApps.AddRange(HiddenApps);

        return _allApps;
    }
    
    protected void RegisterApp<T>() where T : class, IApp
    {
        RegisterMiddle<T>();
    }

    protected void RegisterApp<T>(bool isForClients, bool isForAdmins) where T : class, IApp
    {
        var obj = RegisterMiddle<T>();
        obj.IsForClients = isForClients;
        obj.IsForAdmins = isForAdmins;
    }

    protected void RegisterHidden<T>() where T : class, IApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;

        HiddenApps.Add(obj);
    }

    protected void RegisterTopApp<T>() where T : class, IApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;
        obj.IsForClients = true;
        obj.IsForAdmins = true;

        TopApps.Add(obj);
    }

    protected void RegisterBottomApp<T>() where T : class, IApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;
        obj.IsForClients = true;
        obj.IsForAdmins = true;

        BottomApps.Add(obj);
    }

    protected void RegisterTopApp<T>(bool isForClients, bool isForAdmins) where T : class, IApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;
        obj.IsForClients = isForClients;
        obj.IsForAdmins = isForAdmins;

        TopApps.Add(obj);
    }

    private T RegisterMiddle<T>() where T : class, IApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;
        obj.IsForClients = true;
        obj.IsForAdmins = true;

        MiddleApps.Add(obj);

        return obj;
    }

    private IApp? GetFirstApp(bool isAdminMode)
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

    //private bool HasApp(string appId)
    //{
    //    if (_allApps == null)
    //    {
    //        return false;
    //    }

    //    return _allApps.Any(w => string.Equals(w.Name, appId, StringComparison.InvariantCultureIgnoreCase));
    //}
}