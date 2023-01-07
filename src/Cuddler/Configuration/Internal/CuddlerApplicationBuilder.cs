using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Cuddler.Configuration.Internal;

internal class CuddlerApplicationBuilder
{
    private readonly WebApplicationBuilder _applicationBuilder;

    private readonly ApplicationSettings _applicationSettings;

    private readonly BoostConfiguration _boostConfiguration;

    public CuddlerApplicationBuilder(WebApplicationBuilder applicationBuilder, Action<BoostConfiguration>? action = null)
    {
        action ??= _ => {
        };

        var boostConfiguration = GetBoostConfiguration(applicationBuilder, action);

        _applicationSettings = new ApplicationSettings();

        _applicationBuilder = applicationBuilder;
        _boostConfiguration = boostConfiguration;

        if (!applicationBuilder.Environment.IsDevelopment())
        {
            _applicationBuilder.Logging.AddFilter<ConsoleLoggerProvider>(level => level == _boostConfiguration.LogLevel);
        }

        SetApplicationSettings();
    }

    internal AuthenticationDatabaseBuilder InitServices()
    {
        _applicationBuilder.Services.AddTransient<ISettingsService, SettingsService>();

        // Web Services
        _applicationBuilder.Services.AddTransient<IAppService, AppService>();

        // Boost Platform
        InitServices_LocalizationFeature(_applicationBuilder);


        // Microsoft Platform Features
        _applicationBuilder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        _applicationBuilder.Services.AddMemoryCache();
        _applicationBuilder.Services.AddLocalization();
        _applicationBuilder.Services.AddDistributedMemoryCache();
        _applicationBuilder.Services.AddResponseCaching();
        _applicationBuilder.Services.AddHttpContextAccessor();
        _applicationBuilder.Services.AddHttpsRedirection(options => options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect);
        _applicationBuilder.Services.AddSession();

        // Boost Extended Microsoft Platform Features
        _applicationBuilder.AddJsonSupportToApis();
        _applicationBuilder.AddRazorSupport();
        _applicationBuilder.AddHstsSupport();
        _applicationBuilder.AddLoggingSupport();

        return new AuthenticationDatabaseBuilder(_applicationBuilder, _boostConfiguration, _applicationSettings);
    }

    private static void InitServices_LocalizationFeature(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<DatabaseLocalizerFactory>();
        services.Configure<RequestLocalizationOptions>(options => {
            options.SupportedCultures = new List<CultureInfo>
            {
                new(LanguageCodes.EnglishCanada)
                //new(LanguageCodes.ChinesePrc)
            };

            options.SupportedUICultures = new List<CultureInfo>
            {
                new(LanguageCodes.EnglishCanada)
                //new(LanguageCodes.ChinesePrc)
            };

            options.DefaultRequestCulture = new RequestCulture(LanguageCodes.EnglishCanada, LanguageCodes.EnglishCanada);
            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new CookieRequestCultureProvider()
            };
        });
    }

    private static BoostConfiguration GetBoostConfiguration(WebApplicationBuilder builder, Action<BoostConfiguration> action)
    {
        var boostConfiguration = Activator.CreateInstance<BoostConfiguration>();
        action.Invoke(boostConfiguration);

        boostConfiguration.StartupServices?.InitProgramStartup(builder);

        return boostConfiguration;
    }

    private void SetApplicationSettings()
    {
        var appSettings = _applicationBuilder.BindApplicationSettings();
        _applicationSettings.EnableMigrations ??= appSettings.EnableMigrations;
        _applicationSettings.ShowErrors ??= appSettings.ShowErrors;
        _applicationSettings.ContentRootFolder ??= appSettings.ContentRootFolder;
        if (string.IsNullOrEmpty(_applicationSettings.ContentRootFolder))
        {
            throw new InvalidOperationException($"{nameof(_applicationSettings.ContentRootFolder)} cannot be null. Error: a32d048d-148d-4d13-808f-58f2657a9168");
        }
    }
}