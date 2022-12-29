using Cuddler.Configuration.Internal;
using Cuddler.Data.Context;
using Cuddler.Data.Entities;
using Cuddler.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cuddler.Configuration;

public class AuthenticationDatabaseBuilder
{
    private readonly WebApplicationBuilder _applicationBuilder;
    private readonly ApplicationSettings _applicationSettings;
    private readonly BoostConfiguration _boostConfiguration;

    public AuthenticationDatabaseBuilder(WebApplicationBuilder applicationBuilder, BoostConfiguration boostConfiguration, ApplicationSettings applicationSettings)
    {
        _applicationBuilder = applicationBuilder;
        _boostConfiguration = boostConfiguration;
        _applicationSettings = applicationSettings;
    }

    public AdditionalDatabaseBuilder InitAuthenticationDatabase<TDbContext>(DatabaseType databaseType) where TDbContext : IdentityDbContext<AccountEntity>, IRepository, ITranslationRepository
    {
        _applicationBuilder.AddAuthenticationDatabase<TDbContext, IRepository>(databaseType, _applicationSettings);
        _applicationBuilder.AddAuthenticationScheme<TDbContext, AccountEntity>(_applicationSettings);

        return new AdditionalDatabaseBuilder(_applicationBuilder, _boostConfiguration, _applicationSettings, databaseType);
    }
}