using System.Diagnostics;
using Cuddler.Data.Repository;
using Cuddler.Web.Language;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cuddler.Web.Configuration.Internal;

internal static class AddRepositoryContextExtension
{
    [DebuggerStepThrough]
    public static void AddAuthenticationDatabase<TDbContext, TRepository>(this WebApplicationBuilder builder, DatabaseType databaseType, ApplicationSettings ApplicationSettings) where TDbContext : DbContext, TRepository, ITranslationRepository where TRepository : class
    {
        // Database
        builder.Services.AddDbContext<TDbContext>(options => {
            switch (databaseType)
            {
                case DatabaseType.SQLite:
                    options.UseSqlite($"Data Source={Path.Combine(ApplicationSettings.ContentRootFolder!, "Db", "boostdc.db")}");

                    break;
                case DatabaseType.SQLServer:
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
            }

            if (builder.Environment.IsDevelopment())
            {
                options.LogTo(Console.WriteLine)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors();
            }
            else
            {
                options.LogTo(Console.WriteLine, LogLevel.None);
            }
        });

        builder.Services.AddScoped<TRepository>(p => p.GetRequiredService<TDbContext>());
        builder.Services.AddScoped<ITranslationRepository>(p => p.GetRequiredService<TDbContext>());
        builder.Services.AddScoped<DbContext>(p => p.GetRequiredService<TDbContext>());
        builder.Services.AddTransient<LanguageService>();

        //builder.Services.AddScoped<ITranslationRepository>(p => p.GetRequiredService<TDbContext>()); // TODO for language support
        // Database Filters
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }

    [DebuggerStepThrough]
    public static void AddAdditionalDatabase<TDbContext>(this WebApplicationBuilder builder, DatabaseType databaseType, ApplicationSettings ApplicationSettings) where TDbContext : DbContext
    {
        // Database
        builder.Services.AddDbContext<TDbContext>(options => {
            switch (databaseType)
            {

                case DatabaseType.SQLite:
                    options.UseSqlite($"Data Source={Path.Combine(ApplicationSettings.ContentRootFolder!, "Db", "boostdc.db")}");
                    break;
                case DatabaseType.SQLServer:
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
            }

            if (builder.Environment.IsDevelopment())
            {
                options.LogTo(Console.WriteLine)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors();
            }
            else
            {
                options.LogTo(Console.WriteLine, LogLevel.None);
            }
        });

        builder.Services.AddScoped(p => p.GetRequiredService<TDbContext>());

        // Database Filters
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }
}