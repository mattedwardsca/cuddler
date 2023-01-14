using CuddlerDev.Configuration.Internal;
using CuddlerDev.Data.Context;
using CuddlerDev.Data.Entities;
using CuddlerDev.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CuddlerDev.Configuration;

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

    public AdditionalDatabaseBuilder InitAuthenticationDatabase<TDbContext>() where TDbContext : IdentityDbContext<AccountEntity>, IRepository, ITranslationRepository
    {
        _applicationBuilder.AddAuthenticationDatabase<TDbContext, IRepository>();
        _applicationBuilder.AddAuthenticationScheme<TDbContext, AccountEntity>(_applicationSettings);

        return new AdditionalDatabaseBuilder(_applicationBuilder, _boostConfiguration, _applicationSettings);
    }
}