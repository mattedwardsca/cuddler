using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Modules;

public interface ICuddlerModule
{
    List<IApp> BottomApps { get; }

    List<IApp> HiddenApps { get; }

    List<IApp> MiddleApps { get; }

    string RootLink { get; }

    bool ShowAdminToggle { get; }

    string SiteName { get; }

    List<IApp> TopApps { get; }

    Task<string> GetRootPage(HttpRequest request);

    List<IApp> ListAllApps();
}