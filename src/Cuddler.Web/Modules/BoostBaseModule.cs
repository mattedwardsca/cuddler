using Cuddler.Core.Services.Modules.Models;

namespace Cuddler.Web.Modules;

public abstract class BoostBaseModule : BaseModule
{
    protected BoostBaseModule(string siteName, string rootLink = "/") : base(siteName, rootLink)
    {
    }

    protected void RegisterApp<T>() where T : class, IClientApp
    {
        RegisterMiddle<T>();
    }

    protected void RegisterApp<T>(bool isForClients, bool isForAdmins, bool isAlwaysPinned) where T : class, IClientApp
    {
        var obj = RegisterMiddle<T>();
        obj.IsForClients = isForClients;
        obj.IsForAdmins = isForAdmins;
    }

    protected void RegisterHidden<T>() where T : class, IClientApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;

        HiddenApps.Add(obj);
    }

    protected void RegisterTopApp<T>() where T : class, IClientApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;

        TopApps.Add(obj);
    }

    protected void RegisterTopApp<T>(bool isForClients, bool isForAdmins) where T : class, IClientApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;
        obj.IsForClients = isForClients;
        obj.IsForAdmins = isForAdmins;

        TopApps.Add(obj);
    }

    private T RegisterMiddle<T>() where T : class, IClientApp
    {
        var obj = (T)Activator.CreateInstance(typeof(T))!;

        MiddleApps.Add(obj);

        return obj;
    }
}