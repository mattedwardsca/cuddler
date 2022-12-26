using Microsoft.AspNetCore.Http;

namespace Cuddler.Core.Modules;

public interface IBoostModule
{
    bool AllowAdminMode { get; }

    string? Description { get; set; }

    List<IClientApp> HiddenApps { get; }

    List<IClientApp> MiddleApps { get; }

    bool RequiresSubscription { get; }

    string RootLink { get; }

    string SiteName { get; }

    string? Slogan { get; set; }

    List<IClientApp> TopApps { get; }

    string? Icon { get; }

    string GetModuleSubscriptionId();

    Task<string> GetRootPage(HttpRequest request);

    bool HasApp(string appId);

    List<IClientApp> ListAllApps();
}