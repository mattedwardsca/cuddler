using Cuddler.Web.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cuddler.Configuration;

public static class UseSettingsFromDatabaseExtension
{
    internal static void LoadWebsiteSettingsFromDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var settingsService = serviceScope.ServiceProvider.GetRequiredService<ISettingsService>();
        var WebsiteSettings = serviceScope.ServiceProvider.GetRequiredService<WebsiteSettings>();

        settingsService.InitWebsiteSettings(WebsiteSettings);
    }
}