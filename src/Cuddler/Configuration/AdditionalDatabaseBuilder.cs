﻿using Cuddler.Configuration.Internal;
using Cuddler.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cuddler.Configuration;

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

    public void Run<TModule>(Action<IApplicationBuilder>? middlewareAction = null) where TModule : class, ICuddlerModule
    {
        var instance = (ICuddlerModule)Activator.CreateInstance(typeof(TModule))!;
        _applicationBuilder.Services.AddSingleton(instance);

        var app = _applicationBuilder.Build();
        app.ConfigureApp(_applicationBuilder.Environment, _boostConfiguration, _applicationSettings, _authenticationDatabaseType, middlewareAction);
        app.Run();
    }
}