using Cuddler.Core.Services.Modules.Models;
using Cuddler.Core.Utils;
using Cuddler.Web.Configuration.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cuddler.Web.Configuration;

public class AdditionalDatabaseBuilder
{
    private readonly WebApplicationBuilder _applicationBuilder;
    private readonly ApplicationSettings _applicationSettings;
    private readonly DatabaseType _authenticationDatabaseType;
    private readonly BoostConfiguration _boostConfiguration;

    public AdditionalDatabaseBuilder(WebApplicationBuilder applicationBuilder, BoostConfiguration boostConfiguration, ApplicationSettings applicationSettings, DatabaseType authenticationDatabaseType)
    {
        _applicationBuilder = applicationBuilder;
        _boostConfiguration = boostConfiguration;
        _applicationSettings = applicationSettings;
        _authenticationDatabaseType = authenticationDatabaseType;
    }

    public AdditionalDatabaseBuilder InitAdditionalDatabase<TDbContext>(DatabaseType databaseType) where TDbContext : DbContext
    {

        _applicationBuilder.AddAdditionalDatabase<TDbContext>(databaseType, _applicationSettings);

        return this;
    }

    public void Run<TModule>(Action<IApplicationBuilder>? middlewareAction = null) where TModule : class, IBoostModule
    {
        var instance = AssemblyScannerUtil.CreateInstance<IBoostModule>(typeof(TModule));
        _applicationBuilder.Services.AddSingleton(instance);

        var app = _applicationBuilder.Build();
        app.ConfigureApp(_applicationBuilder.Environment, _boostConfiguration, _applicationSettings, _authenticationDatabaseType, middlewareAction);
        app.Run();
    }
}