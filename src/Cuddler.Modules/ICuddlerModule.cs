using Microsoft.AspNetCore.Http;

namespace Cuddler.Modules;

public interface ICuddlerModule
{
    bool AllowAdminMode { get; }

    string? Description { get; set; }

    List<IApp> HiddenApps { get; }

    List<IApp> MiddleApps { get; }

    bool RequiresSubscription { get; }

    string RootLink { get; }

    string SiteName { get; }

    string? Slogan { get; set; }

    List<IApp> TopApps { get; }

    string? Icon { get; }

    string GetModuleSubscriptionId();

    Task<string> GetRootPage(HttpRequest request);

    bool HasApp(string appId);

    List<IApp> ListAllApps();
}