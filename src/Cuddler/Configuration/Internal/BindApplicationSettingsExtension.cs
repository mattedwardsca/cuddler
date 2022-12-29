using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cuddler.Configuration.Internal;

internal static class BindApplicationSettingsExtension
{
    public static ApplicationSettings BindApplicationSettings(this WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.BindSection<ApplicationSettings>();
        builder.Services.AddSingleton(appSettings);

        return appSettings;
    }

    /// <summary>
    ///     Utility class that wraps Microsofts ConfigurationBinder.Get Method
    ///     https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder.get?view=dotnet-plat-ext-5.0
    /// </summary>
    public static T BindSection<T>(this IConfiguration configuration)
    {
        var section = (T)(Activator.CreateInstance(typeof(T)) ?? throw new InvalidOperationException());
        configuration.Bind(typeof(T).Name, section);
        return section;
    }
}