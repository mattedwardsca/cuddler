using Cuddler.Configuration.Internal;
using Cuddler.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cuddler.Configuration;

public class AdditionalDatabaseBuilder
{
    private readonly WebApplicationBuilder _applicationBuilder;
    private readonly ApplicationSettings _applicationSettings;
    private readonly BoostConfiguration _boostConfiguration;

    public AdditionalDatabaseBuilder(WebApplicationBuilder applicationBuilder, BoostConfiguration boostConfiguration, ApplicationSettings applicationSettings)
    {
        _applicationBuilder = applicationBuilder;
        _boostConfiguration = boostConfiguration;
        _applicationSettings = applicationSettings;
    }

    public AdditionalDatabaseBuilder InitAdditionalDatabase<TDbContext>() where TDbContext : DbContext
    {

        _applicationBuilder.AddAdditionalDatabase<TDbContext>();

        return this;
    }

    public void Run<TModule>(Action<IApplicationBuilder>? middlewareAction = null) where TModule : class, ICuddlerModule
    {
        var instance = (ICuddlerModule)Activator.CreateInstance(typeof(TModule))!;
        _applicationBuilder.Services.AddSingleton(instance);

        var app = _applicationBuilder.Build();
        app.LoadWebsiteSettingsFromDatabase();
        app.ConfigureApp(_applicationBuilder.Environment, _boostConfiguration, _applicationSettings, middlewareAction);
        app.Run();
    }
}