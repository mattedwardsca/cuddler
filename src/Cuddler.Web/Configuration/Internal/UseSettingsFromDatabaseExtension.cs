using Cuddler.Core.Services.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cuddler.Web.Configuration.Internal;

internal static class UseSettingsFromDatabaseExtension
{
    public static void UseSettingsFromDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                    .CreateScope();

        var settingsService = serviceScope.ServiceProvider.GetRequiredService<ISettingsService>();

        var globalSettings = serviceScope.ServiceProvider.GetRequiredService<GlobalSettings>();
        var dbGlobalSettings = settingsService.GetSettingsObject(globalSettings);

        settingsService.SetObject(dbGlobalSettings);
    }
}